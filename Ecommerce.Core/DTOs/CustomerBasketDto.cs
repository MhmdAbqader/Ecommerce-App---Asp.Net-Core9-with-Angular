using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string userId { get; set; }
        public List<BasketItemDto> BasketItemsDto { get; set; } = new List<BasketItemDto>();
    }
}
