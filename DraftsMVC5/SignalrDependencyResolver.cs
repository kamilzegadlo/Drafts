using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.SignalR;

namespace DraftsMVC5
{
    public class SignalrDependencyResolver : DefaultDependencyResolver
    {
        private readonly Castle.MicroKernel.IKernel _kernel;

        public SignalrDependencyResolver(Castle.MicroKernel.IKernel kernel)
        {
            _kernel = kernel;
        }

        public override object GetService(System.Type serviceType)
        {
            return _kernel.HasComponent(serviceType) ? _kernel.Resolve(serviceType) : base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var objects = _kernel.HasComponent(serviceType) ? _kernel.ResolveAll(serviceType).Cast<object>() : new object[] { };
            return objects.Concat(base.GetServices(serviceType));
        }
    }
}