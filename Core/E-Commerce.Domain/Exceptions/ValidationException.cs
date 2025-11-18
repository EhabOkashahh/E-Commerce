using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions
{
    public class ValidationException : Exception
    {

        public ValidationException(string error) : base(error)
        {
            
        }
        public ValidationException(IEnumerable<string> errors) : base(String.Join(",", errors))
        {
            
        }
    }
}
