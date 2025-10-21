using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOS.Product;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Mapping.Products
{
    internal class ProductPictureUrlResolver(IConfiguration config) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{config["BaseUrl"]}/{source.PictureUrl}";
            }
            return String.Empty;
        }
    }
}
