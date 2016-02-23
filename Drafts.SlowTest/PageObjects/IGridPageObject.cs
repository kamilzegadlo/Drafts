using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.SlowTest.PageObjects
{
    public interface IGridPageObject : IMasterPageObject
    {
        String GetCellText(int pageNo, int rowNo, int cellNo);

        void ClickPage(int PageNumber);

        void ClickLastPage();

        void ClickEdit(int id);

        void ClickDelete(int id);
    }
}
