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
    public class LoginSteps : ABaseTest
    {
        private IHomePageObject home;
        private ILoginPageObject login;

        [Given(@"I have entered URL: (.*)")]
        public void GivenIHaveEnteredURLHttpLocalhost(string url)
        {
           driver.Navigate().GoToUrl(url);
        }

        [Given(@"I have clicked loginLink on Home Page")]
        public void GivenIHaveClickedLoginLinkOnHomePage()
        {
            home.ClickLoginButton();
        }
        
        [Given(@"I have typed (.*) into the UserName on Login Page")]
        public void GivenIHaveTypedKzIntoTheUserNameOnLoginPage(string userName)
        {
            login.TypeUserName(userName);
        }
        
        [Given(@"I have typed (.*) into the Password on Login Page")]
        public void GivenIHaveTypedKzkzkzIntoThePasswordOnLoginPage(string password)
        {
            login.TypePassword(password);
        }
        
        [When(@"I click LoginSubmit on Login Page")]
        public void WhenIClickLoginSubmitOnLoginPage()
        {
            login.ClickLogin();
        }
        
        [Then(@"the UserName should be (.*) on Home Page")]
        public void ThenTheUserNameShouldBeKzOnHomePage(string userName)
        {
            Assert.AreEqual(home.GetUserName(), userName);
        }

        [Given(@"I have logged in")]
        public void GivenIHaveLoggedIn()
        {
            base._login();
        }

        [When(@"I have clicked LogoutButton on Home Page")]
        public void WhenIHaveClickedLogoutButtonOnHomePage()
        {
            home.ClickLogOutButton();
        }

        [Then(@"the UserName should not be displayed on Home Page")]
        public void ThenTheUserNameShouldNotBeDisplayedOnHomePage()
        {
            Assert.Catch(typeof(NoSuchElementException), () => home.GetUserName());
        }

        [BeforeScenario("Login")]
        public void SetUp()
        {
            base.SetUp(false);
            home = PageObjectFactory.GetPageObject<IHomePageObject>(driver);
            login = PageObjectFactory.GetPageObject<ILoginPageObject>(driver);
        }

        [AfterScenario("Login")]
        public void TearDown()
        {
            base.TearDown(false);
        }
    }
}
