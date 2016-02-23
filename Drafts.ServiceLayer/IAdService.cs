using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drafts.ModelLayer;

namespace Drafts.ServiceLayer
{
    public interface IAdService
    {
        IEnumerable<Advert> GetListOfAd(int pageNumber, int recordsOnPage);

        int GetCountOfAd();

        IEnumerable<Advert> GetListOfAd(AdSearchParams searchParams, int pageNumber, int recordsOnPage);

        int GetCountOfAd(AdSearchParams searchParams);

        IEnumerable<User> GetSellerList();

        Advert GetAd(int adId);

        void AddAd(Advert ad);

        void SaveAd(Advert ad);

        void DeleteAd(int id);

        void Dispose();
    }
}
