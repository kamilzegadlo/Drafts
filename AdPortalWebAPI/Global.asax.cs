using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
//using AutoMapper.Configuration;
using Drafts.ModelLayer;
using System.IO;
using Drafts.DataAccessLayer;
using Castle.Windsor;
using Castle.Windsor.Installer;
using AdPortalWebAPI.Plumbing;
using System.Web.Http.Dispatcher;

namespace AdPortalWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;

        protected void Application_Start()
        {
            BootstrapContainer();

            GlobalConfiguration.Configuration.Services.Replace(
            typeof(IHttpControllerActivator),
            new WindsorCompositionRoot(_container));

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AppDomain.CurrentDomain.SetData("DataDirectory", (new FileInfo(AppDomain.CurrentDomain.BaseDirectory)).Directory.Parent.FullName + @"\Drafts.DataAccessLayer");

            AutoMapperWebConfiguration.Configure();

            GlobalConfiguration.Configuration.EnsureInitialized(); 
        }

        private static void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This())
                .Install(FromAssembly.Named("Drafts.ServiceLayer"))
                .Install(FromAssembly.Named("Drafts.DataAccessLayer"));
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_End()
        {
            _container.Dispose();
        }
    }
}
