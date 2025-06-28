using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models.OrderOperations
{
    public class Order:BaseEntity<int>
    {
        public Order()
        {
            //this.Id=0;
        }

        public Order(string customerEmail, decimal subTotal, DeliveryMethod deliveryMethod, ShipToAddress shipToAddress, IReadOnlyList<OrderLine> orderLines)
        {
            CustomerEmail = customerEmail;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
          
            ShipToAddress = shipToAddress;
            OrderLines = orderLines;
        }

        public string CustomerEmail { get; set; }
        public decimal SubTotal{ get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus OrderStatusss { get; set; } = OrderStatus.Pending;
        public ShipToAddress ShipToAddress { get; set; }
        public IReadOnlyList<OrderLine> OrderLines { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.DeliveryPrice;
    }
}
