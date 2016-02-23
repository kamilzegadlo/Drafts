using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DraftsMVC5.ViewModels
{
    public class AdViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Seller { get; set; }
        public decimal Price { get; set; }
    }
}