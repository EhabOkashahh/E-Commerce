using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specification.Orders
{
    public class OrderWithPaymentIntentSpecifications : BaseSepcification<Guid , Order>
    {
        public OrderWithPaymentIntentSpecifications(string PaymentIntentId) : base(o => o.PaymentIntenetId == PaymentIntentId)
        {
            
        }
    }
}
