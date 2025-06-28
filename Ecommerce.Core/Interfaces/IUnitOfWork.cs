using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IBasketRepository BasketRepository{ get; }
        IOrderRepository OrderRepository { get; }
        //IOrderDetailsRepository OrderDetailsRepository { get; }
        int Complete();
    }
}
