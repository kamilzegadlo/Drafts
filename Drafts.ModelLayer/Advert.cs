using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drafts.ModelLayer
{
    public class Advert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("Seller")]
        public string SellerID { get; set; }
        public decimal Price { get; set; }

        public virtual User AspNetUser { get; set; }
    }
}
