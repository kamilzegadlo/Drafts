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
    public static class WebDriverFactory
    {
        public static IWebDriver GetWebDriver()
        {
            return new FirefoxDriver();
        }
    }
}
