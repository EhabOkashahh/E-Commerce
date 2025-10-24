using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Aabstractions.Product
{
    public interface IProductServices
    {
        Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParam param);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
        Task<IEnumerable<BrandTypeResponse>> GetAllCategoriesAsync();
    }
}
