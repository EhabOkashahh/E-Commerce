using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts
{
    public interface IGenericRepository<TKey,TEntity>  where TEntity : BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false);
        Task<TEntity> GetAsync(TKey key);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
