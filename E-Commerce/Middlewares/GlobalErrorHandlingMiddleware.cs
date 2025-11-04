using Azure;
using E_Commerce.Domain.Exceptions;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Server.IIS;
using System.Runtime.InteropServices;

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
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandlingNotFoundErrorAsync(context);
                }
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized) 
                {
                    await HandlingUnAuthExceptionsAsync(context);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandlingExceptionsErrorAsync(context, ex);
            }
        }

        private static async Task HandlingExceptionsErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message,
            };

            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundErrorAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"This URL {context.Request.Path} is Not Found",
            };
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingUnAuthExceptionsAsync(HttpContext context)
        {
            context.Request.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                ErrorMessage = "UnAuthorized! Are you Forgot to Login?",
                StatusCode = StatusCodes.Status401Unauthorized
            };

           await context.Response.WriteAsJsonAsync(response);
        }
    }
}
