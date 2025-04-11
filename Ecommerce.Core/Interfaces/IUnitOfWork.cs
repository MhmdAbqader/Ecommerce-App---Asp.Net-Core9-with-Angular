﻿using System;
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
        //IShoppingCartRepository ShoppingCartRepository { get; }
        //IOrderHeaderRepository OrderHeaderRepository { get; }
        //IOrderDetailsRepository OrderDetailsRepository { get; }
        int Complete();
    }
}
