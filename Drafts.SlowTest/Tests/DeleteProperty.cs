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
    [TestFixture]
    public class DeleteProperty : ABaseTest
    {
        [TearDown]
        public void TestCleanup()
        {
            DatabaseSnapshot.RestoreSnapShot();
        }

        [Test]
        public void WD_DeleteProperty()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            IDeletePageObject delete = PageObjectFactory.GetPageObject<IDeletePageObject>(driver);
            IGridPageObject grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);
            ISearchPageObject search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);

            home.WaitUntilPageReady();

            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test13");
            search.TypePriceFrom(1);
            search.TypePriceTo(300000);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();

            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test13");
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 1, 0));

            grid.ClickDelete(2);

            delete.WaitUntilPageReady();

            delete.ClickDeleteButton();

            grid.WaitUntilPageReady();

            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test13");
            search.TypePriceFrom(1);
            search.TypePriceTo(300000);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();

            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 0, 0));
        }
    }
}
