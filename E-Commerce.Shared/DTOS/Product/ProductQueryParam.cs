using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOS.Product
{
    //int? brandId , int? typeId , string? sort,string? searchText,int? pageIndex = 1 , int? pageSize =6
    public class ProductQueryParam
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? Sort { get; set; }
        public string? searchText { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 6;
    }
}
