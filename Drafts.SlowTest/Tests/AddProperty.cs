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
using System.Data.Entity;
using Drafts.DataAccessLayer;
using System.Transactions;

namespace Drafts.SlowTest
{
    // Not testing the commit makes the whole test a half integration test... 
    [TestFixture]
    public class AddProperty : ABaseTest
    {
        [TearDown]
        public void TestCleanup()
        {
            DatabaseSnapshot.RestoreSnapShot();
        }

        [Test]
        public void WD_AddProperty()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            ICreatePageObject create = PageObjectFactory.GetPageObject<ICreatePageObject>(driver);
            IGridPageObject grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);
            ISearchPageObject search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);

            home.WaitUntilPageReady();

            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test_title_toRemove");
            search.TypePriceFrom(99000);
            search.TypePriceTo(101000);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 1, 0));

            home.ClickCreateTab();

            create.WaitUntilPageReady();
            create.TypeTitle("test_title_toRemove");
            create.TypeDescription("test_description_toRemove");
            create.TypePrice(100000);
            create.ClickCreateButton();

            grid.WaitUntilPageReady();

            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test_title_toRemove");
            search.TypePriceFrom(99000);
            search.TypePriceTo(101000);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();
            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test_title_toRemove");
            Assert.AreEqual(grid.GetCellText(0, 0, 1), "test_description_toRemove");
            Assert.AreEqual(grid.GetCellText(0, 0, 2), "kz");
            Assert.AreEqual(grid.GetCellText(0, 0, 3), "100000.00");
        }
    }
}
