using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drafts.SlowTest.PageObjects;
using NUnit.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumWebdriverHelpers;

namespace Drafts.SlowTest
{
    [TestFixture]
    public class SearchPropertyAd : ABaseTest
    {
        [Test]
        public void WD_SearchAdCheckIfResultIsOnlyOne()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            ISearchPageObject search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);
            IGridPageObject grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);

            home.WaitUntilPageReady();
            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test13");
            search.TypePriceFrom(1);
            search.TypePriceTo(999999);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();
            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test13");
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 1, 0));
        }

        [Test]
        public void WD_SearchAdCheckIfManyResults()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            ISearchPageObject search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);
            IGridPageObject grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);

            home.WaitUntilPageReady();
            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test");
            search.TypePriceFrom(1);
            search.TypePriceTo(999999);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();
            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test");
            Assert.AreEqual(grid.GetCellText(0, 1, 0), "test13");
        }

        [Test]
        public void WD_SearchAdCheckIfResultIsEmpty()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            ISearchPageObject search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);
            IGridPageObject grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);

            home.WaitUntilPageReady();
            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test13");
            search.TypePriceFrom(1);
            search.TypePriceTo(2);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 0, 0));
        }

    }
}
