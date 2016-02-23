using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class HomePageObject : AMasterPageObject, IHomePageObject
    {
        public HomePageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle = "Home Page";
        }
    }
}
