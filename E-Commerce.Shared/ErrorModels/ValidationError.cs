using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.ErrorModels
{
    public class ValidationError
    {
        public string Filed { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
