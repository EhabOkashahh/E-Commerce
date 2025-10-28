using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repository
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var Value = await _database.StringGetAsync(key);
            return !Value.IsNullOrEmpty ? Value : default;
            
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var redisValue = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, redisValue, duration);
        }
    }
}
