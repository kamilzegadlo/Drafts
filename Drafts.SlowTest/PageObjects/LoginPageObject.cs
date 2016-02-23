using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class LoginPageObject : AMasterPageObject, ILoginPageObject
    {
        public LoginPageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle = "Log in";
        }

        public void TypeUserName(string userName)
        {
            SendKeysToElement("UserName", userName);
        }

        public void TypePassword(string password)
        {
            SendKeysToElement("Password", password);
        }

        public void ClickLogin()
        {
            ClickElement("LoginSubmit");
        }
    }
}
