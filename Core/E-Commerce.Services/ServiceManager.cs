using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Aabstractions.Baskets;
using E_Commerce.Services.Auth;
using E_Commerce.Services.Baskets;
using E_Commerce.Services.Cache;
using E_Commerce.Services.Orders;
using E_Commerce.Services.Payments;
using E_Commerce.Services.Products;
using E_Commerce.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ServiceManager(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                IBasketRepository basketRepository,
                                ICacheRepository cacheRepository,
                                IOptions<JwtOptions> options,
                                UserManager<AppUser> userManager,
                                IConfiguration config) : IServiceManager
    {
        public IProductServices ProductServices { get; } = new ProductService(unitOfWork, mapper);
        public IBasketServices BasketServices { get; } = new BasketService(basketRepository, mapper);
        public ICacheServices CacheServices { get; } = new CacheService(cacheRepository);
        public IAuthServices AuthServices { get; } = new AuthService(userManager,options, mapper);
        public IOrderServices OrderServices { get; } = new OrderService(unitOfWork,mapper,basketRepository);
        public IPayementSecivce payementSecivce { get; } = new PaymentService(basketRepository, unitOfWork, config, mapper);
    }
}
    