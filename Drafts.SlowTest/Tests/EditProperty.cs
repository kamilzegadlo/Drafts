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
    public class EditProperty : ABaseTest
    {
        [TearDown]
        public void TestCleanup()
        {
            DatabaseSnapshot.RestoreSnapShot();
        }

        [Test]
        public void WD_EditProperty()
        {
            IHomePageObject home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            IEditPageObject edit = PageObjectFactory.GetPageObject<IEditPageObject>(driver);
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

            Assert.AreNotEqual(grid.GetCellText(0, 0, 1), "test_description_Edit");

            grid.ClickEdit(2);

            edit.WaitUntilPageReady();

            edit.TypeDescription("test_description_Edit");

            edit.ClickEditButton();

            grid.WaitUntilPageReady();

            home.ClickSearchTab();

            search.WaitUntilPageReady();
            search.TypeTitle("test13");
            search.TypePriceFrom(1);
            search.TypePriceTo(300000);
            search.ClickSearchButton();

            grid.WaitUntilPageReady();

            Assert.AreEqual(grid.GetCellText(0, 0, 1), "test_description_Edit");
        }
    }
}
