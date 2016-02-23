using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Drafts.SlowTest.PageObjects;

namespace Drafts.SlowTest
{
    public abstract class ABaseTest
    {
        protected IWebDriver driver;

        protected virtual void SetUp(Boolean withLogIn) 
        {
            driver = WebDriverFactory.GetWebDriver();
            if(withLogIn)
                _login();
        }

        [SetUp]
        protected virtual void SetUp()
        {
            SetUp(true);
        }

        protected virtual void TearDown(Boolean withLogOut)
        {
            try
            {
                if (withLogOut)
                    _logOut();
            }
            finally
            {
                driver.Quit();
            }
        }

        [TearDown]
        protected virtual void TearDown()
        {
            TearDown(true);
        }

        protected void _login()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            ILoginPageObject login = PageObjectFactory.GetPageObject<ILoginPageObject>(driver);

            driver.Navigate().GoToUrl(Constans.url);
            home.WaitUntilPageReady();
            home.ClickLoginButton();
            login.WaitUntilPageReady();
            login.TypeUserName(Constans.userName);
            login.TypePassword(Constans.password);
            login.ClickLogin();
        }

        protected void _logOut()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);

            home.ClickLogOutButton();
        }
    }
}
