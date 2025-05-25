using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Product : BaseEntity<int>
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price {get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string ImgURL { get; set; }

    }
}
