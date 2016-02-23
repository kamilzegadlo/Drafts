using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drafts.ServiceLayer;
using Microsoft.AspNet.SignalR;

namespace DraftsMVC5.Hubs
{
    public class AdHub : Hub
    {
        //Dependency injection to property not a constructor.
        public IAdService _adService { get; set; }

        public void getActualCountOfAds()
        {
            Clients.Caller.setActualCountOfAds(_adService.GetCountOfAd());
        }
    }
}