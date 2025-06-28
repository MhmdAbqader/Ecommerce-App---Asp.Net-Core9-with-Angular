using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class OrderCreateDto
    {
        public int DeliveryMethodId { get; set; }
        public string BasketId { get; set; }  // customerId in Basket 
        public AddressDto ShipToAddressDto { get; set; }
    }
}
