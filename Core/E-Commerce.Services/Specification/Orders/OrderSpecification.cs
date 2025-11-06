using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Shared.DTOS.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specification.Orders
{
    public class OrderSpecification : BaseSepcification<Guid,Order>
    {
        public OrderSpecification(Guid id , string email) : base(o => o.Id == id && o.UserEmail.ToLower() == email.ToLower())
        {
            IncludeExpressions.Add(o => o.DeliveryMethod);
            IncludeExpressions.Add(o => o.Items);
        }
        public OrderSpecification(string email) : base(o => o.UserEmail.ToLower() == email.ToLower())
        {
            IncludeExpressions.Add(o => o.DeliveryMethod);
            IncludeExpressions.Add(o => o.Items);

            AddOrderBydesc(o => o.OrderDate);
        }
    }
}
