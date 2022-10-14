using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using EPG_Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPG_Api.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApikey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Api Key Was Not Privided!..."
                };
                return;
            }
            Models.EPGContext client = new Models.EPGContext();
            //var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            var apiKey = client.Users.Where(w => w.Apikey.Equals(extractedApikey)).Select(s => s.Apikey).FirstOrDefault();
            if (!apiKey.Equals(extractedApikey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    Content = "Unauthorized Client!..."
                };
                return;
            }
            await next();
        }
    }
}
