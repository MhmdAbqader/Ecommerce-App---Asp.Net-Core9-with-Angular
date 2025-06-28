using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Core.Models.OrderOperations
{
    public class OrderLine : BaseEntity<int>
    {
        public OrderLine()
        {            
        }

        public OrderLine(ProductItemOrderd productItemOrderd, decimal price, int quantity)
        {
            ProductItemOrderd = productItemOrderd;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrderd ProductItemOrderd { get; set; }
        public decimal Price{ get; set; }
        public int Quantity{ get; set; }

        // [NotMapped]
        [Column]
        public Nullable<int> OrderId { get; set; }

    }
}