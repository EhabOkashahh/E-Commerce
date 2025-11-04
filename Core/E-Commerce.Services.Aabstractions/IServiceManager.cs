using E_Commerce.Services.Aabstractions.Baskets;
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
        ICacheServices CacheServices { get; }
        IAuthServices AuthServices { get; }

    }
}
