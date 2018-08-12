using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Bitcoin.Wallet.Api.Helpers
{
    public class StructureMapResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapResolver(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new StructureMapResolver(child);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }
            else
            {
                return _container.GetInstance(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.GetAllInstances(serviceType).Cast<object>();
            }
            catch
            {
                return new List<object>();
            }
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}