using CVideoAPI.Services.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVideoAPI.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheClearingAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
     
            var executedContext = await next();

            if (executedContext.Result is NoContentResult || executedContext.Result is CreatedResult)
            {
                await cacheService.RemoveCache(context.HttpContext.Request.Path);
            }
        }
    }
}
