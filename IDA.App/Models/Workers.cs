using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Worker
    {
        public Worker()
        {
            JobOffers = new List<JobOffer>();
            WorkerServices = new List<WorkerService>();
        }

        public int Id { get; set; }
        public double RadiusKm { get; set; }
        public DateTime IsAvailbleUntil { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<WorkerService> WorkerServices { get; set; }
    }
}
