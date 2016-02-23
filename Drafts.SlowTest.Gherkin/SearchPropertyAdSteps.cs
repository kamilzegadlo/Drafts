using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using Drafts.SlowTest;
using Drafts.SlowTest.PageObjects;
using NUnit.Framework;

namespace Drafts.SlowTest.Gherkin
{
    [Binding]
    public class SearchPropertyAdSteps : ABaseTest
    {
        private ISearchPageObject search;
        private IGridPageObject grid;

        [Given(@"I have typed (.*) into the Title on Search Page")]
        public void GivenIHaveTypedTestIntoTheTitleOnSearchPage(string title)
        {
            search.TypeTitle(title);
        }
        
        [Given(@"I have typed (.*) into the Price From on Search Page")]
        public void GivenIHaveTypedIntoThePriceFromOnSearchPage(double priceFrom)
        {
            search.TypePriceFrom(priceFrom);
        }
        
        [Given(@"I have typed (.*) into the Price To on Search Page")]
        public void GivenIHaveTypedIntoThePriceToOnSearchPage(double priceTo)
        {
            search.TypePriceTo(priceTo);
        }
        
        [When(@"I click Search Button on Search Page")]
        public void WhenIClickSearchButtonOnSearchPage()
        {
            search.ClickSearchButton();
        }
        
        [Then(@"the result should be empty")]
        public void ThenTheResultShouldBeEmpty()
        {
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 0, 0));
        }
        
        [Then(@"the result should should contain many hits")]
        public void ThenTheResultShouldShouldContainManyHits()
        {
            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test");
            Assert.AreEqual(grid.GetCellText(0, 1, 0), "test13");
        }
        
        [Then(@"the result should should contain one hit")]
        public void ThenTheResultShouldShouldContainOneHit()
        {
            Assert.AreEqual(grid.GetCellText(0, 0, 0), "test13");
            Assert.Catch(typeof(NoSuchElementException), () => grid.GetCellText(0, 1, 0));
        }

        [BeforeScenario("Search")]
        public void setUp()
        {
            base.SetUp();
            search = PageObjectFactory.GetPageObject<ISearchPageObject>(driver);
            grid = PageObjectFactory.GetPageObject<IGridPageObject>(driver);
            search.ClickSearchTab();
        }

        [AfterScenario("Search")]
        public void TearDown()
        {
            base.TearDown();
        }
    }
}
