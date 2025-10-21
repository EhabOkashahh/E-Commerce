using Azure.Core.Pipeline;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.Contexts;
using E_Commerce.Persistence.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        public ConcurrentDictionary<string , object> Dictionary = new ConcurrentDictionary<string, object>();
        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {
            return (IGenericRepository<TKey, TEntity>) Dictionary.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TKey, TEntity>(_context));
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
