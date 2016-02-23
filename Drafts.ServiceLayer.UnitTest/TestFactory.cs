using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Drafts.ServiceLayer;
using Drafts.DataAccessLayer;

namespace Drafts.ServiceLayer.UnitTest
{
    public class TestFactory
    {
        private static IWindsorContainer container;
        private static MockObjectSelector _mockObjectSelector;

        public static void Clear()
        {
            container = null;
            _mockObjectSelector = null;
        }

        private static IWindsorContainer WindsorContainer
        {
            get
            {
                if (container == null)
                    container = Create();
                return container;
            }
        }

        private static IWindsorContainer Create()
        {
            var windsorContainer = new WindsorContainer();

            _mockObjectSelector = new MockObjectSelector();
            windsorContainer.Kernel.AddHandlerSelector(_mockObjectSelector);

            windsorContainer.Register(
                Component.For<IAdService>().ImplementedBy<AdService>(),
                Component.For<Entities>().ImplementedBy<Entities>()
                );

            return windsorContainer;
        }

        public static T Get<T>()
        {
            return WindsorContainer.Resolve<T>();
        }

        public static void RegisterMock(object mock)
        {
            IWindsorContainer container1 = WindsorContainer;
            _mockObjectSelector.Register(mock);
        }

    }
}
