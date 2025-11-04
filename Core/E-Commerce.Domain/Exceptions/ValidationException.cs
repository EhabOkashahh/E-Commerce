using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions
{
    public class ValidationException(IEnumerable<string> errors) : Exception(String.Join(",",errors))
    {
    }
}
