using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drafts.SlowTest.PageObjects;
using NUnit.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest
{
    [TestFixture]
    public class Login : ABaseTest
    {
        [TearDown]
        protected override void TearDown()
        {
            base.TearDown(false);
        }

        [Test]
        public void WD_LogIn()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);

            home.WaitUntilPageReady();
            Assert.AreEqual(home.GetUserName(),Constans.userName);
            
            _logOut();
        }

        [Test]
        public void WD_LogOut()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);

            home.WaitUntilPageReady();
            home.ClickLogOutButton();

            home.WaitUntilPageReady();

            Assert.Catch(typeof(NoSuchElementException),()=>home.GetUserName());
        }


    }
}
