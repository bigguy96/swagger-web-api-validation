using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FileUploadWebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private const string apiHeader = "api-key";
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(apiHeader, out var apiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //var key = config.GetValue<string>("api");

            if (!apiKey.Equals("test"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
