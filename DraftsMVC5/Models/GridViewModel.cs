using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drafts.ModelLayer;
using Drafts.ServiceLayer;

namespace DraftsMVC5.ViewModels
{
    public class GridViewModel
    {
        private int _recordsOnPage;
        private IEnumerable<Advert> _ads;
        private int _pageCount;
        public GridViewModel(IEnumerable<Advert> ads, int recordCount, int recordsOnPage)
        {
            _recordsOnPage = recordsOnPage;
            _ads = ads;

            if (recordCount > 0)
            {
                _pageCount = recordCount / _recordsOnPage;
                if (recordCount % _recordsOnPage > 0)
                    ++_pageCount;
            }
        }

        public IEnumerable<Advert> Ads { get { return _ads; } }

        public int CurrentPage { get; set; }

        public int PageCount { get { return _pageCount; } }

        public AdSearchParams SearchParams { get; set; }
    }
}