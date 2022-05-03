using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Attributtes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class ApiKeyAttributte : Attribute, IAsyncActionFilter
    {
        //creacion de un atributo que luego nos funciona como decoracion para nuestros controladores
        //e inyectarle el mecanismo de seguridad por ApiKey

        //nombre apikey
        private const string ApiKeyName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var ApiOut))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No se ha incluido una API Key"
                };

                return;

            }

            //
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKeyName);

            if (!apiKey.Equals(ApiOut))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "La API Key suministrada no es la correcta. Intente de nuevo!"
                };

                return;
            }

            await next();
        }
    }
}
