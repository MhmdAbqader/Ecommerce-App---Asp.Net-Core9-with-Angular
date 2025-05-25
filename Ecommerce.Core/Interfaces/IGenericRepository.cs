using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Models;

namespace Ecommerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<IReadOnlyList<T>> GetAllAsync(); // just for read only 
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> criteria, string? includeTable = null);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes); //.another way to send includeable property
       
        Task  AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task  UpdateAsync(int id, T entity);
        Task  DeleteAsync(int id);

    }
}
