using Ecommerce.Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Core.DTOs
{
    public class BaseProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,9999 ,ErrorMessage ="price must be more than ZERO and less than 10000")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage ="Must be Number")]
        public decimal Price { get; set; }
        public string Img { get; set; } = "No Img Added!";
    }
    public class ProductDto : BaseProduct
    {
        public int Id { get; set; }            
                
        public string CategoryName { get; set; }

        public int Totalcount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class CreateProductDto : BaseProduct
    {
        public int CategoryId { get; set; }
        public IFormFile ImgURL { get; set; }
    }
}
