using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.DTOs;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        // our new function related to only product
        Task<IEnumerable<ProductDto>> GetAllAsync(string search, string sort, int? catId, int pageNo, int pageSize);
        Task<bool> AddAsync(CreateProductDto createProductDto);
        Task<bool> DeleteWithImageAsync(int id);
    }
}
