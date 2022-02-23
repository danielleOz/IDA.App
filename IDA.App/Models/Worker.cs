using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Worker:User
    {
        public Worker()
        {
            JobOffers = new List<JobOffer>();
            WorkerServices = new List<WorkerService>();
        }

        public double RadiusKm { get; set; }
        public DateTime IsAvailbleUntil { get; set; }

        public List<JobOffer> WorkerJobOffers { get; set; }
        public List<WorkerService> WorkerServices { get; set; }
    }
}
