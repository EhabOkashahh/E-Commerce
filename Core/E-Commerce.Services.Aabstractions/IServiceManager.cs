using E_Commerce.Services.Aabstractions.Baskets;
using E_Commerce.Services.Aabstractions.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions
{
    public interface IServiceManager
    {
        IProductServices ProductServices { get; }
        IBasketServices BasketServices { get; }

    }
}
