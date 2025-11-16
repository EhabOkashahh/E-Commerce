using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Middlewares;
using E_Commerce.Persistence;
using E_Commerce.Persistence.Identity;
using E_Commerce.Services;
using E_Commerce.Shared;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
            services.AddIdentityService();

            services.ConfigureService();
            services.AddAuthenticationServices(configuration);

            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services , IConfiguration configuration)
        {
            var JwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "Bearer";
                o.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = JwtOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey)),

                        ValidateLifetime = true,
                    };
                });
            
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
        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
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
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
        private static async Task<WebApplication> InitializeDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitializer.InitializeAsync();
            await DbInitializer.InitializeIdentityAsync();

            return app;
        }
        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }

        
    }
}
