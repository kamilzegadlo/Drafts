using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Drafts.SlowTest.PageObjects
{
    public abstract class ABasePageObject
    {
        protected IWebDriver _driver;
        protected string _pageTitle;

        protected ABasePageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public virtual void WaitUntilPageReady()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.Until((d) =>
            {
                return d.Title.Equals(_pageTitle);
            });
        }

        protected void ClickElement(string elementId)
        {
            _driver.FindElement(By.Id(elementId)).Click();
        }

        protected void SendKeysToElement(string elementId, string value)
        {
            IWebElement element = _driver.FindElement(By.Id(elementId));

            element.Clear();
            element.SendKeys(value);
        }

        protected void SelectValueFromDropDownList(string elementId, string value)
        {
            foreach (IWebElement option in _driver.FindElement(By.Id(elementId)).FindElements(By.TagName("option")))
                if (option.GetAttribute("value").Equals(value))
                {
                    option.Click();
                    break;
                }
        }

        protected string GetTextFromElement(string elementId)
        {
            return _driver.FindElement(By.Id(elementId)).Text;
        }

    }
}
