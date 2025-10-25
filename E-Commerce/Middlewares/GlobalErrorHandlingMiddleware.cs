using Azure;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Server.IIS;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound) {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        ErrorMessage = $"This URL {context.Request.Path} is Not Found",
                    };
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message,
                };

                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };

                context.Response.StatusCode = response.StatusCode;

               await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
