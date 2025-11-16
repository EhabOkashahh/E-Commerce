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

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }   
        public string? SearchText { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
