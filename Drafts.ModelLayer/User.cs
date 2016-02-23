using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Drafts.ModelLayer
{
    public partial class User
    {
        public User()
        {
            this.Ads = new HashSet<Advert>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Advert> Ads { get; set; }
    }
}
