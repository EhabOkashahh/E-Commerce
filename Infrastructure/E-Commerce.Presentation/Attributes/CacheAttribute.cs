using E_Commerce.Services.Aabstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    public class CacheAttribute(int duration) : Attribute, IAsyncActionFilter
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheServices;

            var CacheKey = GenerateCacheKey(context.HttpContext.Request);

            var result = await CacheService.GetCacheValueAsync(CacheKey);
            if(!String.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }
            var ExecutingResult = await next.Invoke();
            if (ExecutingResult.Result is OkObjectResult okObjectResult) 
                await CacheService.SetCacheValueAsync(CacheKey, okObjectResult.Value, TimeSpan.FromSeconds(duration));

        }


        private string GenerateCacheKey(HttpRequest request)
        {
            var queryPartsCombined = request.Query.OrderBy(q => q.Key).Aggregate("", (acc, q) => string.IsNullOrEmpty(acc) ? $"{q.Value}" : $"{acc}-{q.Value}");

            return $"{queryPartsCombined}";
        }
    }
}
