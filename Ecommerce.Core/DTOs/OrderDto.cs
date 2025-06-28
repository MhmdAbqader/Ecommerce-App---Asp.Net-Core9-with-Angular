using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDto ShipToAddressDto { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderLineDto> OrderLinesDto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string OrderStatus { get; set; }
    }
}
