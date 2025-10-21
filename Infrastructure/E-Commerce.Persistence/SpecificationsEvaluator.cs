using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TKey , TEntity>(IQueryable<TEntity> inputQuery ,ISpecifications<TKey,TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; // dbcontext.Entity
            if(spec.FilterationExpression is not null)
            {
                query = query.Where(spec.FilterationExpression);
            }

            query = spec.IncludeExpressions.Aggregate(query,(query , Exp) => query.Include(Exp));

            return query;
        }
    }
}
