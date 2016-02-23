using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Drafts.ServiceLayer;
using Drafts.ModelLayer;
using Drafts.DataAccessLayer;
using AdPortalWebAPI.DTOs;
using AutoMapper;
using Microsoft.AspNet.Identity;
using AdPortalWebAPI.Exceptions;

namespace AdPortalWebAPI.Controllers
{
    [Authorize]
    public class AdController : ApiController
    {
        public IAdService _adService { get; set; }

        public AdController(IAdService adService)
        {
            _adService = adService;
        }


        [HttpGet]
        [ActionName("GetSellers")]
        [AllowAnonymous]
        public IEnumerable<SellerDTO> GetSellers()
        {
            return _adService.GetSellerList().Select(a => Mapper.Map<SellerDTO>(a));
        }

        // GET api/values
        [HttpGet]
        [ActionName("GetAds")]
        [AllowAnonymous]
        public IEnumerable<AdDTO> GetAds(int pageNumber, int pageSize, [FromUri]AdSearchParams searchParams)
        {
            return _adService.GetListOfAd(searchParams, pageNumber-1, pageSize).ToList().Select(a => Mapper.Map<AdDTO>(a));
        }

        // GET api/values/5
        [HttpGet]
        [AllowAnonymous]
        public AdDTO GetAd(int id)
        {
            return Mapper.Map<AdDTO>(_adService.GetAd(id));
        }

        // GET api/values
        [HttpGet]
        [ActionName("GetTotalItems")]
        [AllowAnonymous]
        public int GetTotalItems()
        {
            return _adService.GetCountOfAd();
        }

        // POST api/values
        [HttpPost]
        public void AddAd([FromBody]AdDTO value)
        {
            Advert ad = Mapper.Map<Advert>(value);
            ad.SellerID = User.Identity.GetUserId();

            _adService.AddAd(ad);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPut]
        public void EditAd([FromBody]AdDTO value)
        {
            if (value.SellerID != User.Identity.GetUserId())
                throw new SecurityException("Not an Owner tried to edit Ad.");

            _adService.SaveAd(Mapper.Map<Advert>(value));
        }

        // DELETE api/values/5
        [HttpDelete]
        public void DeleteAd(int id)
        {
            if (_adService.GetAd(id).SellerID != User.Identity.GetUserId())
                throw new SecurityException("Not an Owner tried to delete Ad.");

            _adService.DeleteAd(id);
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete()
        {
        }
    }
}
