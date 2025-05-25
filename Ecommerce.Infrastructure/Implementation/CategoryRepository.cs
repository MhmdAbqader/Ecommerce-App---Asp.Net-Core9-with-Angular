using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<Category> GetByIdAsync(int id, params Expression<Func<int, object>>[] includes)
        //{
        //    var category = await _context.Categories.FindAsync(id);
        //    return category;
        //}
    }
}
