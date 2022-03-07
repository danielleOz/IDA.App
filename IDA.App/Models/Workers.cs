using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Workers
    {
        public Workers()
        {
            JobOffers = new List<JobOffer>();
            Reviews = new List<Review>();
            WorkerServices = new List<WorkerService>();
        }

        public int Wid { get; set; }
        public string UserName { get; set; }
        public int Lid { get; set; }
        public string Services { get; set; }
        public double Location { get; set; }


        public virtual Location LidNavigation { get; set; }
        public virtual User UserNameNavigation { get; set; }


        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<WorkerService> WorkerServices { get; set; }
    }
}
