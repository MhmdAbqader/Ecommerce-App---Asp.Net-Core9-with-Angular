using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Models
{
    public class Category : BaseEntity<int>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
