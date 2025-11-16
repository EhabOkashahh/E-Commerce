using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.OrderExceptions
{
    public class DeliveryMethodNotFound(int id) : NotFoundException($"Delivery Method with id {id} Was not found")
    {
    }
}
