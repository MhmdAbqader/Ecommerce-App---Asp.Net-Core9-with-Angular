using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Implementation;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure.Repositories
{
    // i made unit of work to insert all changes to database as bulk insertion 
    // i made unit of work to create DbContext as one time  
    public class UnitOfWork : IUnitOfWork
    {
        
        private readonly ApplicationDbContext _context;
 
        private readonly IFileProvider _fileProvider;
        //private readonly IConnectionMultiplexer _redisConnectionMultiplexer;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }
        public IBasketRepository BasketRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public UnitOfWork( ApplicationDbContext context, IFileProvider fileProvider, IConnectionMultiplexer redis)
        {
           
            _context = context; 
            _fileProvider = fileProvider;             
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context,_fileProvider);
            BasketRepository = new BasketRepository(redis);
           OrderRepository = new OrderRepository(_context, ProductRepository, BasketRepository);
        }


        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
