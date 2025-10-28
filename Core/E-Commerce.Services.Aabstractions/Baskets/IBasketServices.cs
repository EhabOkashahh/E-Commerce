using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions.Baskets
{
    public interface IBasketServices
    {
        Task<BasketDto> GetBasketAsync(string id);
        Task<BasketDto?> CreateBasketAsync(BasketDto Basket, TimeSpan timeToLive);
        Task<bool> DeleteBasketAsync(string id);
    }
}
