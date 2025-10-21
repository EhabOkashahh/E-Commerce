using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specification
{
    public class BaseSepcification<TKey , TEntity> : ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? FilterationExpression { get; set; }

        public BaseSepcification(Expression<Func<TEntity, bool>>? expression)
        {
            FilterationExpression = expression;
        }
    }
}
