using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DraftsMVC5.Plumbing;
using System.IO;

namespace DraftsMVC5
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            BootstrapContainer();

            var signalrDependency = new SignalrDependencyResolver(_container.Kernel);
            Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver = signalrDependency;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppDomain.CurrentDomain.SetData("DataDirectory", (new FileInfo(AppDomain.CurrentDomain.BaseDirectory)).Directory.Parent.FullName + @"\Drafts.DataAccessLayer");

            AutoMapperWebConfiguration.Configure();
        }

        private static void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This())
                .Install(FromAssembly.Named("Drafts.ServiceLayer"))
                .Install(FromAssembly.Named("Drafts.DataAccessLayer"));
            //RouteTable.Routes.MapHubs();//new CastleWindsorDependencyResolver(_container)
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}
