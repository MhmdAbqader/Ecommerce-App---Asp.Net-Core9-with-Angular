namespace Ecommerce.Core.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName{ get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}