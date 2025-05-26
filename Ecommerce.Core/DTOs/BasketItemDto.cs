using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        //[Required]
       // public string ProductPicture { get; set; }
        [Required]
        [Range(0.1, 10000, ErrorMessage = "Price Must Greater Thant Zero")]
        public decimal Price { get; set; }
        [Range(1, 5010, ErrorMessage = "Quantity Must Greater Thant Zero")]
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string categoryName { get; set; }
    }
}