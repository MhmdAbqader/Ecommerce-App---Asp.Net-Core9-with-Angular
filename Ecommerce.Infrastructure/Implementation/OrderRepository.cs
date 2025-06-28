using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models.OrderOperations;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Implementation
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;

        public OrderRepository(ApplicationDbContext context, IProductRepository productRepository, IBasketRepository basketRepository) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethodId, string basketId, ShipToAddress shipToAddress)
        {

            //var testGetOrderLineReltedWithOrder = _context.Orders.Include(a=>a.OrderLines).Where(a=>a.OrderLines.Where(x=>x.Id == 2).Select(a=> new
            //{
            //    ord = a.Id, emil =a.CustomerEmail, ordLineProduct = a.OrderLines.Select(a=>a.ProductItemOrderd.ProductItemName),
            //    ordLinePrice =  a.OrderLines.Select(a=>a.Price),
            //    ordLineQuntity = a.OrderLines.Select(a=>a.Quantity)
            //}).ToList();

            // if you have no direct relation or Navigation Property in orderLine to Order  
            // u can add new property and add this [Column] above it to orderLine to represent orderId to get OrderLine related to order like produt and category  
            // var orders = _context.Orders.Include(a=>a.OrderLines).ToList();
            //var orderLineList = _context.OrderLines.ToList();

            //var getOrderLineReltedWithOrder = _context.OrderLines.Where(a=>a.Id == 3).FirstOrDefault();
            //var orddd = _context.Orders.FirstOrDefault(a => a.Id == getOrderLineReltedWithOrder.OrderId);
            //var res = new { orderid = orddd.Id, ordedate = orddd.OrderDate, orderlineid = getOrderLineReltedWithOrder.Id, price = getOrderLineReltedWithOrder.Price };
          
            
            // get all items in basket related to customer
            // basketId here is the id of customer 
            var customerBsktItems = await _basketRepository.GetBasketAsync(basketId);

   
            if (customerBsktItems is null) return null;

            List<OrderLine> orderLines = new();
            foreach (var item in customerBsktItems.BasketItems)
            {
                var productItem = await _productRepository.GetByIdAsync(p => p.Id == item.Id);
                ProductItemOrderd productItemOrderd = new ProductItemOrderd(productItem.Id, productItemName: productItem.Name);
                orderLines.Add(new OrderLine(productItemOrderd, item.Price, item.Quantity)); 
            }
            var subTotal = orderLines.Sum(a => a.Quantity * a.Price);
            var deliverMethod = await _context.DeliveryMethods.Where(a => a.Id == deliveryMethodId).FirstOrDefaultAsync();
            Order order = new Order(customerEmail, subTotal, deliverMethod, shipToAddress, orderLines);

            if(order is null) return null;


            // OrderLinesis Saved to Db without _context.OrderLines.Add(OrderLine) or _context.OrderLines.AddRange(OrderLine) // why ?????????

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // delete Basket Items for user after Creating his Order
            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();            
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail)
        {
            var orders = await _context.Orders.Where(a => a.CustomerEmail == customerEmail)
                                              .Include(a => a.OrderLines)
                                              .Include(a => a.DeliveryMethod).ToListAsync();
            return orders;
        }

        //Not Working and go direct to CreateOrderAsync Method // but i don't know the reason  
        //
        //public override async Task<Order> GetByIdAsync(Expression<Func<Order, bool>> criteria, string? includeTable = null)
        //{
        //     return base.GetByIdAsync(criteria, includeTable).Result;

        //    //var order = await _context.Orders.Where(criteria).Include(a=>a.OrderLines)
        //    //                                                    .Include(a=>a.DeliveryMethod).FirstOrDefaultAsync();
        //    //return order;
        //}
     
    }
}
