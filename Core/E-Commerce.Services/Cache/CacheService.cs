using E_Commerce.Domain.Contracts;
using E_Commerce.Services.Aabstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Cache
{
    public class CacheService(ICacheRepository repository) : ICacheServices
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
           var value = await repository.GetAsync(key);
            return value is null ? null : value;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
        {
           await repository.SetAsync(key, value, duration);
        }
    }
}
