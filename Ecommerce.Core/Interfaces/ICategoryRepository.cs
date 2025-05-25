using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        // our new function related to only category
        // this is tip to pass T as integer not as category in function GetById

        //Task<Category> GetByIdAsync(int id, params Expression<Func<int, object>>[] includes);
    }
}
