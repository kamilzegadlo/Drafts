using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Handlers;

namespace Drafts.ServiceLayer.UnitTest
{
    public class MockObjectSelector : IHandlerSelector
    {
        private readonly List<object> mocks = new List<object>();

        public bool HasOpinionAbout(string key, Type service)
        {
            if (service == typeof(object)) return false;
            foreach (var o in mocks)
            {
                if (service.IsInstanceOfType(o))
                    return true;
            }
            return false;
        }

        public IHandler SelectHandler(string key, Type service, IHandler[] handlers)
        {
            foreach (var o in mocks)
            {
                if (service.IsInstanceOfType(o))
                    return new MockHandler(o);
            }
            throw new InvalidOperationException("could not find matching mock");
        }

        public void Register(object mock)
        {
            mocks.Add(mock);
        }

        public void ClearMocks()
        {
            mocks.Clear();
        }


        internal class MockHandler : AbstractHandler
        {
            private readonly object _mock;

            public MockHandler(object mock)
                : base(new ComponentModel(new ComponentName("mock", true), new[] { mock.GetType() }, mock.GetType(), new HybridDictionary()))
            {
                _mock = mock;
            }

            public override bool ReleaseCore(Burden burden)
            {
                return true;
            }

            protected override object Resolve(CreationContext context, bool instanceRequired)
            {
                return _mock;
            }
        }
    }
}
