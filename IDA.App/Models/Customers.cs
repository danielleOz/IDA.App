using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Customers
    {
        public Customers()
        {
            JobOffers = new List<JobOffer>();
            Reviews = new List<Review>();
        }

        public int Cid { get; set; }
        public string UserName { get; set; }
        public int Lid { get; set; }

        public virtual Location LidNavigation { get; set; }
        public virtual User UserNameNavigation { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
