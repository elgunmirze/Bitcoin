using Bitcoin.Wallet.Api.App_Start;
using System.Web;
using System.Web.Http;

namespace Bitcoin.Wallet.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Configure();
        }
    }
}
