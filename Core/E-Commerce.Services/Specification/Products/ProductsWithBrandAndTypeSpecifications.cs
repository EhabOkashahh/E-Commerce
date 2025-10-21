using E_Commerce.Domain.Entities.Products;
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
        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId, string? sort, string? searchText) : base(
                p =>
                (!brandId.HasValue || p.BrandId == brandId)
                &&
                (!typeId.HasValue || p.TypeId == typeId)
                &&
                (string.IsNullOrEmpty(searchText) || p.Name.ToLower().Contains(searchText.ToLower()))
            )
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "pricedesc":
                        AddOrderBydesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p=>p.Name);
            }
                ApplyInclude();
        }

        private void ApplyInclude()
        {
            IncludeExpressions.Add(P => P.Brand);
            IncludeExpressions.Add(P => P.Type);
        }
    }
}
