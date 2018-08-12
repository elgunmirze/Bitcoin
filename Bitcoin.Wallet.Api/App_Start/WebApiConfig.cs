using Bitcoin.Wallet.Api.App_Start;
using Bitcoin.Wallet.Api.Respository;
using System.Web.Http;

namespace Bitcoin.Wallet.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            new DbFill();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            ContainerConfig.Register(config);

            config.Formatters.JsonFormatter.
                SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }
    }
}
