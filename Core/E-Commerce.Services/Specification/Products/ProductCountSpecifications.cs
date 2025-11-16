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
                (!param.BrandId.HasValue || p.BrandId == param.BrandId)
                &&
                (!param.TypeId.HasValue || p.TypeId == param.TypeId)
                &&
                (string.IsNullOrEmpty(param.SearchText) || p.Name.ToLower().Contains(param.SearchText.ToLower()))
            )
        {
            
        }
    }
}
