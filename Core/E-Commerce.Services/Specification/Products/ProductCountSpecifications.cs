using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specification.Products
{
    public class ProductCountSpecifications : BaseSepcification<int,Product>
    {
        public ProductCountSpecifications(ProductQueryParam param) : base(
                p =>
                (!param.brandId.HasValue || p.BrandId == param.brandId)
                &&
                (!param.typeId.HasValue || p.TypeId == param.typeId)
                &&
                (string.IsNullOrEmpty(param.searchText) || p.Name.ToLower().Contains(param.searchText.ToLower()))
            )
        {
            
        }
    }
}
