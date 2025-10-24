using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repository
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return ChangeTracker ? await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync() as IEnumerable<TEntity> :
                                   await _context.Products.Include(p => p.Brand).Include(p => p.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return ChangeTracker ? await _context.Set<TEntity>().ToListAsync() :
                                   await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey,TEntity> spec,bool ChangeTracker = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        public async Task<TEntity> GetAsync(TKey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.Brand).Include(p => p.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
        }
        public async Task<TEntity> GetAsync(ISpecifications<TKey,TEntity> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
             await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<int> CountAsync(ISpecifications<TKey, TEntity> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }



        // Method to apply spec
        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TKey,TEntity> spec)
        {
            return SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), spec);
        }

    }
}
