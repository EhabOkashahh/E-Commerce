using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection service, IConfiguration configurations)
        {
            service.AddDbContext<StoreDbContext>(o =>
            {
                o.UseSqlServer(configurations.GetConnectionString("DefaultConnection"));
            });
            service.AddScoped<IDbInitializer, DbInitializer>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddSingleton<IConnectionMultiplexer>(serviceProvider => ConnectionMultiplexer.Connect(configurations.GetConnectionString("Redis")!));
           
            return service;
        }
    }
}
