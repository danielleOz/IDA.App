using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class JobOfferStatus
    {
        public JobOfferStatus()
        {
            JobOffers = new List<JobOffer>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}