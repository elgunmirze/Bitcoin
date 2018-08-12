using Bitcoin.Wallet.Api.Helpers;
using Bitcoin.Wallet.Api.Interfaces;
using Bitcoin.Wallet.Api.Respository;
using StructureMap;
using StructureMap.Graph;
using System.Web.Http;

namespace Bitcoin.Wallet.Api.App_Start
{
    public class ContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            container.Configure(configure =>
            {
                configure.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                });
            });

            config.DependencyResolver = new StructureMapResolver(container);
        }
    }
}