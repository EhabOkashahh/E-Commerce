using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Aabstractions.Product;
using E_Commerce.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ServiceManager(IUnitOfWork unitOfWork , IMapper mapper) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductService(unitOfWork , mapper);
    }
}
