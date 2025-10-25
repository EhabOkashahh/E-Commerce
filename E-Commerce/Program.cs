
using E_Commerce.Domain.Contracts;
using E_Commerce.Extentions;
using E_Commerce.Middlewares;
using E_Commerce.Persistence;
using E_Commerce.Persistence.Data.Contexts;
using E_Commerce.Services;
using E_Commerce.Services.Aabstractions;
using E_Commerce.Services.Mapping.Products;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // add service to the container
            builder.Services.AddAllServices(builder.Configuration);




            var app = builder.Build();
            // Configure the HTTP request pipeline.
            await app.ConfigureMiddlewareAsync();
            app.Run();

            
        }
    }
}
