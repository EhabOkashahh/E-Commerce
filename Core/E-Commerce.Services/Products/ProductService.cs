using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Exceptions.ProductExceptions;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Specification.Products;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Products
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper _mapper) : IProductServices
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductAsync(ProductQueryParam param)
        {

            var spec = new ProductsWithBrandAndTypeSpecifications(param);

            var products = await unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var Response = _mapper.Map<IEnumerable<ProductResponse>>(products);

            var CountSpec = new ProductCountSpecifications(param);
            var CountBeforePagination =await unitOfWork.GetRepository<int, Product>().CountAsync(CountSpec); 

            return new PaginationResponse<ProductResponse>(param.PageSize,param.PageIndex,CountBeforePagination,Response);
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {

            var spec = new ProductsWithBrandAndTypeSpecifications(id);

            var product = await unitOfWork.GetRepository<int,Product>().GetAsync(spec);
            if (product == null) throw new ProductNotFoundException(id);
            var response = _mapper.Map<ProductResponse>(product);
            return response;
        }   
        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var response = _mapper.Map<IEnumerable<BrandTypeResponse>>(brands);
            return response;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllCategoriesAsync()
        {
            var types =await unitOfWork.GetRepository<int , ProductType>().GetAllAsync();
            var response = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return response;
        }

       
    }
}
