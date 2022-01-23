using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class WorkerService
    {
        public WorkerService()
        {
            JobOffers = new List<JobOffer>();
        }

        public int Swid { get; set; }
        public int Sid { get; set; }
        public int Wid { get; set; }
        public double Price { get; set; }

        public virtual Service SidNavigation { get; set; }
        public virtual Workers WidNavigation { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
