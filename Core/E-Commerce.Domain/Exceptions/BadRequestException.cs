﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Exceptions
{
    public abstract class BadRequestException(string message) : Exception(message)
    {
    }
}
