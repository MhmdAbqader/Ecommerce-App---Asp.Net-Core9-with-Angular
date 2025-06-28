using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models.OrderOperations;

namespace Ecommerce.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethodId, string basketId, ShipToAddress shipToAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
