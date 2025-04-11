using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        // our new function related to only category
    }
}
