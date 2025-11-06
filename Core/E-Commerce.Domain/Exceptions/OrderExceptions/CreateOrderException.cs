using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.OrderExceptions
{
    public class CreateOrderException() : BadRequestException("SomeThing went wrong! , Cannot create this Order")
    {
    }
}
