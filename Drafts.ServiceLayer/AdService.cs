using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drafts.DataAccessLayer;
using Drafts.ModelLayer;
using System.Data.Entity;
using AutoMapper;

namespace Drafts.ServiceLayer
{
    public class AdService : IAdService, IDisposable
    {
        private Entities _db;

        public AdService(Entities db)
        {
            _db = db;

            //where is a good place to configure automapper in service?
            AutoMapperServiceConfiguration.Configure();
        }

        public IEnumerable<Advert> GetListOfAd(int pageNumber, int recordsOnPage)
        {
            return _db.Ads.Include(o => o.AspNetUser).OrderBy(o => o.Id).Skip(pageNumber * recordsOnPage).Take(recordsOnPage).ToList().Select(a => Mapper.Map<Advert>(a));
        }

        public int GetCountOfAd()
        {
            return _db.Ads.Count();
        }

        public IEnumerable<Advert> GetListOfAd(AdSearchParams searchParams, int pageNumber, int recordsOnPage)
        {
            if (searchParams == null)
                return GetListOfAd(pageNumber, recordsOnPage);

            return _db.Ads.Include(o => o.AspNetUser).Where(o =>
                (String.IsNullOrEmpty(searchParams.Title) || o.Title.Contains(searchParams.Title))
                && (String.IsNullOrEmpty(searchParams.Description) || o.Description.Contains(searchParams.Description))
                && (searchParams.PriceFrom == 0 || o.Price >= searchParams.PriceFrom)
                && (searchParams.PriceTo == 0 || o.Price <= searchParams.PriceTo)
                && (String.IsNullOrEmpty(searchParams.SellerID) || o.SellerID == searchParams.SellerID)
            ).OrderBy(o => o.Id).Skip(pageNumber * recordsOnPage).Take(recordsOnPage).ToList().Select(a => Mapper.Map<Advert>(a));
        }

        public int GetCountOfAd(AdSearchParams searchParams)
        {
            if (searchParams == null)
                return GetCountOfAd();

            return _db.Ads.Include(o => o.AspNetUser).Where(o =>
                (String.IsNullOrEmpty(searchParams.Title) || o.Title.Contains(searchParams.Title))
                && (String.IsNullOrEmpty(searchParams.Description) || o.Description.Contains(searchParams.Description))
                && (searchParams.PriceFrom == 0 || o.Price >= searchParams.PriceFrom)
                && (searchParams.PriceTo == 0 || o.Price <= searchParams.PriceTo)
                && (String.IsNullOrEmpty(searchParams.SellerID) || o.SellerID == searchParams.SellerID)
            ).Count();
        }

        public IEnumerable<User> GetSellerList()
        {
            return _db.AspNetUsers.ToList().Select(u => Mapper.Map<User>(u));
        }

        public Advert GetAd(int adId)
        {
            return Mapper.Map<Advert>(_db.Ads.FirstOrDefault(a=>a.Id==adId));
        }

        public void AddAd(Advert ad)
        {
            _db.Ads.Add(Mapper.Map<Ad>(ad));
            _db.SaveChanges();
        }

        public void SaveAd(Advert ad)
        {
            Ad a = _db.Ads.Where(aa => aa.Id == ad.Id).AsQueryable().FirstOrDefault();
            _db.Entry(a).CurrentValues.SetValues(ad);  //EF already includes a way to map properties without resorting to Automapper
            _db.SaveChanges();
        }

        public void DeleteAd(int id)
        {
            Ad ad = _db.Ads.Find(id);
            _db.Ads.Remove(ad);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}