using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace AdPortalWebAPI.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestyleTransient());
        }
    }
}