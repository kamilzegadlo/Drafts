using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public abstract class AMasterPageObject : ABasePageObject, IMasterPageObject
    {
        public AMasterPageObject(IWebDriver driver)
            : base(driver){}

        public void ClickLoginButton()
        {
            ClickElement("loginLink");
        }

        public void ClickLogOutButton()
        {
            ClickElement("LogoutButton");
        }

        public String GetUserName()
        {
            return GetTextFromElement("UserName");
        }

        public void ClickSearchTab()
        {
            ClickElement("SearchTab");
        }

        public void ClickCreateTab()
        {
            ClickElement("CreateTab");
        }

        public void ClickGridTab()
        {
            ClickElement("GridTab");
        }
    }
}
