using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.SlowTest.PageObjects
{
    public interface ICreatePageObject : IMasterPageObject
    {
        void TypeTitle(string title);

        void TypeDescription(string description);

        void SelectSeller(string sellerId);

        void TypePrice(Double priceFrom);

        void ClickCreateButton();
    }
}
