using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;

namespace Drafts.SlowTest
{
    [SetUpFixture]
    public class AssemblySetUp
    {
        [SetUp]
        public void SetUp()
        {
            DatabaseSnapshot.SetupStoredProcedures();
            DatabaseSnapshot.DeleteSnapShot();
            DatabaseSnapshot.CreateSnapShot();
        }

        [TearDown]
        public void TearDown()
        {
            DatabaseSnapshot.DeleteSnapShot();
        }
    }
}