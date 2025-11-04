using AutoMapper;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Mapping.Baskets;
using E_Commerce.Services.Mapping.Products;
using E_Commerce.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration) {

            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
