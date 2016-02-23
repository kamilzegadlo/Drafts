using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.SlowTest.PageObjects
{
    public interface ILoginPageObject : IMasterPageObject
    {
        void TypeUserName(string userName);

        void TypePassword(string password);

        void ClickLogin();
    }
}
