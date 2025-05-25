using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Core.DTOs
{
    public class CategoryDto  
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ListCategoryDto : CategoryDto
    {
        public int Id { get; set; }
    }
}
