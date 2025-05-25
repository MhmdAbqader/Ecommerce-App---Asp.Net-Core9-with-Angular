using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client.Extensibility;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure.Implementation
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _context;
 
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _context = redis.GetDatabase();      
        } 
        public  async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
           var result = await _context.StringGetAsync(basketId);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(result);
            }
            return null;

            //    return result.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(result);

        }

     
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var basket = await _context.StringSetAsync(customerBasket.UserId,
                                                        JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(45));
            
            if (basket == false)
                return null;
            else
                return await GetBasketAsync(customerBasket.UserId);
        }

        public  async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _context.KeyDeleteAsync(basketId);
        }

    }
}
