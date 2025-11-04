using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions.AuthExceptions
{
    public class InvalidEmailOrPasswordException() : UnAuthorizedException("Invalid Email Or Password!")
    {
    }
}
