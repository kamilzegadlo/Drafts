using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class GridPageObject : AMasterPageObject, IGridPageObject
    {
        public GridPageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle="Grid";
        }

        private IWebElement _propertyAdGrid
        {
            get { return _driver.FindElement(By.Id("PropertyAdGrid")); }
        }

        public String GetCellText(int pageNo,int rowNo, int cellNo)
        {
            return _propertyAdGrid.FindElement(By.Id(cellNo + "-" + pageNo + "-" + rowNo)).GetText();
        }

        public void ClickPage(int PageNumber)
        {
            ClickElement("Page"+PageNumber);
        }

        public void ClickLastPage()
        {
             _driver.FindElements(By.CssSelector("[id*='Page']")).OrderByDescending(p=>p.GetAttribute("id")).First().Click();
        }

        public void ClickEdit(int id)
        {
            ClickElement("Edit" + id);
        }

        public void ClickDelete(int id)
        {
            ClickElement("Delete" + id);
        }

    }
}
