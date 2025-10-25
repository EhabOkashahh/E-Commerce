using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.ProductExceptions
{
    public class ProductNotFoundException(int id) : NotFoundException($"The Product with id = {id} Not Found!")
    {
    }
}
