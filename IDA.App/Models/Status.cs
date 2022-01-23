using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Status
    {
        public Status()
        {
            JobOffers = new List<JobOffer>();
        }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
