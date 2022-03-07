using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Review
    {
        public Review()
        {
            JobOffers = new List<JobOffer>();
        }

        public int Rid { get; set; }
        public int Sid { get; set; }
        public string Sname { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public int Cid { get; set; }
        public int Wid { get; set; }
        public int Jid { get; set; }

        public virtual Customers CidNavigation { get; set; }
        public virtual JobOffer JidNavigation { get; set; }
        public virtual Workers WidNavigation { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
