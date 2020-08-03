using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SampleWebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        private const string apiHeader = "api-key";
        private const string jwtHeader = "app-jwt";
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var hasApiKey = context.HttpContext.Request.Headers.TryGetValue(apiHeader, out var apiKey);
          var hasJwt = context.HttpContext.Request.Headers.TryGetValue(jwtHeader, out var jwt);

            if(!hasApiKey && !hasJwt)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //var key = config.GetValue<string>("api");
            //var key = config.GetValue<string>("jwt");

            if (!apiKey.Equals("test") && !jwt.Equals("test"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
