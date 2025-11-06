using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Exceptions.BasketExceptions;
using E_Commerce.Services.Aabstractions.Baskets;
using E_Commerce.Shared.DTOS.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Baskets
{
    public class BasketService(IBasketRepository repository, IMapper _mapper) : IBasketServices
    {
        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var result = await repository.GetBasketAsync(id);
            var basket = _mapper.Map<BasketDto>(result);
            return result is null ? throw new BasketNotFoundException(id) : basket;
        }
        public async Task<BasketDto?> CreateBasketAsync(BasketDto Basket)
        {
            var basket = _mapper.Map<CustomerBasket>(Basket);
            basket = await repository.CreateBasketAsync(basket , TimeSpan.FromDays(30));

            var result = _mapper.Map<BasketDto>(basket);
            return basket is null ? throw new BasketCreateOrUpdateBadRequestException() : result;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await repository.DeleteBasketAsync(id) ? true : throw new BasketDeleteBadRequestException();
        }
    }
}
