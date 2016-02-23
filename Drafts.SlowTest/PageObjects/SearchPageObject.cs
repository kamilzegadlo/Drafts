using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class SearchPageObject : AMasterPageObject, ISearchPageObject
    {
        public SearchPageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle = "Search";
        }

        public void TypeTitle(string title)
        {
            SendKeysToElement("Title", title);
        }

        public void TypeDescription(string description)
        {
            SendKeysToElement("Description", description);
        }

        public void SelectSeller(string sellerId)
        {
            SelectValueFromDropDownList("SellerID", sellerId.ToString());
        }

        public void TypePriceFrom(Double priceFrom)
        {
            SendKeysToElement("PriceFrom", priceFrom.ToString());
        }

        public void TypePriceTo(Double priceTo)
        {
            SendKeysToElement("PriceTo", priceTo.ToString());
        }

        public void ClickSearchButton()
        {
            ClickElement("SearchBtn");
        }
    }
}
