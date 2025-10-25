using E_Commerce.Domain.Contracts;
using E_Commerce.Middlewares;
using E_Commerce.Persistence;
using E_Commerce.Services;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extentions
{
    public static class Extentions
    {
        // Services
        public static IServiceCollection AddAllServices(this IServiceCollection services , IConfiguration configuration) {

            AddBuiltInService(services);
            AddSwaggerService(services);

            services.AddInfrastructureService(configuration);
            services.AddApplicationServices(configuration);

            ConfigureService(services);

            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfigureService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
               config.InvalidModelStateResponseFactory = (actionContext) =>
               {
                   var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                            .Select(m => new ValidationError()
                                            {
                                                Filed = m.Key,
                                                Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                                            });
                   var response = new ValidationErrorResponse()
                   {
                       Errors = errors
                   };
                   return new BadRequestObjectResult(response);
               }
           );
            return services;
        }



        // MiddleWares
        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            await app.InitializeDataBaseAsync();
            app.UseGlobalErrorHandling();

            app.UseStaticFiles();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
        private static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();

            return app;
        }
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

        
    }
}
