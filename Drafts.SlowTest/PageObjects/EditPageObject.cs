﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest.PageObjects
{
    public class EditPageObject : AMasterPageObject, IEditPageObject
    {
        public EditPageObject(IWebDriver driver)
            : base(driver)
        {
            _pageTitle = "Edit";
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
            SelectValueFromDropDownList("SellerID", sellerId);
        }

        public void TypePrice(Double priceFrom)
        {
            SendKeysToElement("Price", priceFrom.ToString());
        }

        public void ClickEditButton()
        {
            ClickElement("EditBtn");
        }
    }
}
