using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOS.Product;

namespace E_Commerce.Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                        .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                        .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
