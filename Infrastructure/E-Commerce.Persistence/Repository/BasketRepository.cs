using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repository
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;

            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            return basket is null ? null : basket;
        }
        public async Task<CustomerBasket?> CreateBasketAsync(CustomerBasket customerBasket, TimeSpan timeToLive)
        {
            var jsonBasket = JsonSerializer.Serialize(customerBasket);

            var flag = await _database.StringSetAsync(customerBasket.Id, jsonBasket, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(customerBasket.Id) : null;
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
