using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.BasketExceptions
{
    public class BasketNotFoundException(string id) : NotFoundException($"Basket with Id = {id} Not Found!")
    {
    }
}
