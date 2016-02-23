using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class DeletePageObject : AMasterPageObject, IDeletePageObject
    {
        public DeletePageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle = "Delete";
        }

        public void ClickDeleteButton()
        {
            ClickElement("DeleteBtn");
        }
    }
}
