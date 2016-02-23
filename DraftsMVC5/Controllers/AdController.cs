using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Microsoft.Web.WebPages.OAuth;
//using WebMatrix.WebData;
using DraftsMVC5.ViewModels;
using AutoMapper;
using Drafts.ServiceLayer;
using Drafts.ModelLayer;
using Microsoft.AspNet.SignalR;
using DraftsMVC5.Hubs;
using Microsoft.AspNet.Identity;

namespace DraftsMVC5.Controllers
{
    public class AdController : Controller
    {
        private IAdService adService;
        private int _recordsOnPage = 3;//the best place for this would be a settings table in db. 
        //The best would be if we could save user's settings about columns, width, records on page etc. in db.

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        public ActionResult Index()
        {
            return View(new GridViewModel(adService.GetListOfAd(0, _recordsOnPage), adService.GetCountOfAd(), _recordsOnPage));
        }

        public ActionResult GetPageRecords(string searchParamsJson, int pageNumber)
        {
            AdSearchParams searchParams = null;
            if (!String.IsNullOrEmpty(searchParamsJson))
                searchParams = Newtonsoft.Json.JsonConvert.DeserializeObject<AdSearchParams>(searchParamsJson);

            GridViewModel gridViewModel = new GridViewModel(adService.GetListOfAd(searchParams, pageNumber, _recordsOnPage), adService.GetCountOfAd(searchParams), _recordsOnPage);
            gridViewModel.CurrentPage = pageNumber;
            gridViewModel.SearchParams = searchParams;

            return View("Index", gridViewModel);
        }

        public ActionResult Search()
        {
            AdSearchViewModel adSearchViewModel = new AdSearchViewModel();

            adSearchViewModel.SellerList = new SelectList(adService.GetSellerList(), "Id", "UserName", 0);

            return View(adSearchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(AdSearchViewModel searchParamsView)
        {
            AdSearchParams searchParams = Mapper.Map<AdSearchParams>(searchParamsView);
            GridViewModel gridViewModel = new GridViewModel(adService.GetListOfAd(searchParams, 0, _recordsOnPage), adService.GetCountOfAd(searchParams), _recordsOnPage);
            gridViewModel.SearchParams = searchParams;

            return View("Index", gridViewModel);
        }

        //
        // GET: /Detail/Details/5
        public ActionResult Details(int id = 0)
        {
            Advert ad = adService.GetAd(id);

            if (ad == null)
                return HttpNotFound();

            return View(Mapper.Map<AdViewModel>(ad));
        }

        //
        // GET: /Detail/Create
        [System.Web.Mvc.Authorize]
        public ActionResult Create()
        {
            EditAdViewModel adCreateViewModel = new EditAdViewModel();

            adCreateViewModel.SellerList = new SelectList(adService.GetSellerList(), "Id", "UserName");

            return View(adCreateViewModel);
        }

        //
        // POST: /Detail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Authorize]
        public ActionResult Create(AdViewModel adcvm)
        {
            if (ModelState.IsValid)
            {
                Advert ad = Mapper.Map<Advert>(adcvm);
                ad.SellerID = User.Identity.GetUserId(); ;

                adService.AddAd(ad);

                UpdateAdCounterForAllConnectedClients();

                return RedirectToAction("Index");
            }

            return View(adcvm);
        }

        private void UpdateAdCounterForAllConnectedClients()
        {
            var accountContext = GlobalHost.ConnectionManager.GetHubContext<AdHub>();
            accountContext.Clients.All.setActualCountOfAds(adService.GetCountOfAd());
        }

        //
        // GET: /Detail/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Advert ad = adService.GetAd(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            EditAdViewModel editAdViewModel = Mapper.Map<EditAdViewModel>(ad);
            editAdViewModel.SellerList = new SelectList(adService.GetSellerList(), "Id", "UserName", ad.SellerID);
            return View(editAdViewModel);
        }

        //
        // POST: /Detail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditAdViewModel ad)
        {
            if (ModelState.IsValid)
            {
                adService.SaveAd(Mapper.Map<Advert>(ad));
                return RedirectToAction("Index");
            }
            return View(ad);
        }

        //
        // GET: /Detail/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Advert ad = adService.GetAd(id);

            if (ad == null)
                return HttpNotFound();

            return View(Mapper.Map<AdViewModel>(ad));
        }

        //
        // POST: /Detail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            adService.DeleteAd(id);

            UpdateAdCounterForAllConnectedClients();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            adService.Dispose();
            base.Dispose(disposing);
        }
    }

}