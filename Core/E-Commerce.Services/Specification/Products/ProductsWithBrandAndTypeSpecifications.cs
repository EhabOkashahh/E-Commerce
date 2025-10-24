using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specification.Products
{
    public class ProductsWithBrandAndTypeSpecifications : BaseSepcification<int , Product>
    {
        public ProductsWithBrandAndTypeSpecifications(int id) : base(p=>p.Id == id)
        {
            ApplyInclude();
        }
        public ProductsWithBrandAndTypeSpecifications(ProductQueryParam param) : base(
                p =>
                (!param.brandId.HasValue || p.BrandId == param.brandId)
                &&
                (!param.typeId.HasValue || p.TypeId == param.typeId)
                &&
                (string.IsNullOrEmpty(param.searchText) || p.Name.ToLower().Contains(param.searchText.ToLower()))
            )
        {
            
            ApplyInclude();
            ApplySorting(param.Sort);
            ApplyPagination(param.pageSize, param.pageIndex);
        }


        private void ApplyInclude()
        {
            IncludeExpressions.Add(P => P.Brand);
            IncludeExpressions.Add(P => P.Type);
        }
        private void ApplySorting(string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderBydesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
    }
}
