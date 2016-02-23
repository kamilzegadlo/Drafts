using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.SlowTest.PageObjects
{
    public interface IMasterPageObject
    {
        void ClickLoginButton();

        void ClickLogOutButton();

        void ClickSearchTab();

        void ClickCreateTab();

        void ClickGridTab();

        String GetUserName();

        void WaitUntilPageReady();
    }
}
