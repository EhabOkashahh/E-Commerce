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
        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base(
                p =>
                (!brandId.HasValue || p.BrandId == brandId)
                &&
                (!typeId.HasValue || p.TypeId == typeId)
            )
        {
            ApplyInclude();
        }

        private void ApplyInclude()
        {
            IncludeExpressions.Add(P => P.Brand);
            IncludeExpressions.Add(P => P.Type);
        }
    }
}
