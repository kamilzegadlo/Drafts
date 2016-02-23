using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drafts.SlowTest.PageObjects;
using OpenQA.Selenium;

namespace Drafts.SlowTest
{
    public static class PageObjectFactory
    {
        public static T GetPageObject<T>(IWebDriver driver) where T:IMasterPageObject
        {
            if (typeof(IHomePageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new HomePageObject(driver));
            else if (typeof(ILoginPageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new LoginPageObject(driver));
            else if (typeof(ISearchPageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new SearchPageObject(driver));
            else if (typeof(IGridPageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new GridPageObject(driver));
            else if (typeof(ICreatePageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new CreatePageObject(driver));
            else if (typeof(IEditPageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new EditPageObject(driver));
            else if (typeof(IDeletePageObject).IsAssignableFrom(typeof(T)))
                return (T)(object)(new DeletePageObject(driver));
            else
                throw new UnregisteredTypeException(typeof(T).FullName);
        }
    }
}
