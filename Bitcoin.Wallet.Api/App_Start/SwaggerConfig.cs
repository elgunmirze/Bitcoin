using System.Web.Http;
using WebActivatorEx;
using Bitcoin.Wallet.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Bitcoin.Wallet.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Bitcoin.Wallet.Api");
                    })
                .EnableSwaggerUi(c =>
                    {
                    });
        }
    }
}
