using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;
using Ecommerce.Core.Models;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity<int> // i use this instead of class to show
                                                                                        //id property when using getById methos   
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity =await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes) 
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> criteria, string? includeTable = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (criteria != null)
            {
                query = query.Where(criteria);
            }
            if (includeTable != null)
            {
                foreach (var res in includeTable.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(res);
                }
            }
            //return query.SingleOrDefault(); // i change that when creating the cart summary when i need to retun the user from shoppingCart
            return query.FirstOrDefault();
        }

        public async Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[] includes) //.another way to send includeable property
        {
            IQueryable<T> query = _context.Set<T>().Where(a=>a.Id == id);

          
            if (includes != null)
            {
                foreach (var res in includes)
                {
                    query = query.Include(res);
                }
            }
            //return query.SingleOrDefault(); // i change that when creating the cart summary when i need to retun the user from shoppingCart
            return query.FirstOrDefault();
        }

        public async Task UpdateAsync( T entity)
        { 
                _context.Update(entity);
                await _context.SaveChangesAsync();            
        }
        public async Task UpdateAsync(int id, T entity)
        {
            var existEntity = await _context.Set<T>().FindAsync(id);
            if (existEntity is not null) 
            {
                _context.Update(existEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
