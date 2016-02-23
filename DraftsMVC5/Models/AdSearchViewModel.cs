using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DraftsMVC5.ViewModels
{
    public class AdSearchViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? SellerID { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }

        public IEnumerable<SelectListItem> SellerList { get; set; }
    }
}