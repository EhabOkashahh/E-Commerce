using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.BasketExceptions
{
    public class BasketCreateOrUpdateBadRequestException() : BadRequestException("An Error occurred while handle Create or Update Operation")
    {
    }
}
