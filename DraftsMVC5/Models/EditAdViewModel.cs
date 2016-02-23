using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DraftsMVC5.ViewModels
{
    public class EditAdViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SellerID { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<SelectListItem> SellerList { get; set; }

    }
}