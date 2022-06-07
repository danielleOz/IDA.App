using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Worker : User
    {
        public Worker()
        {
            JobOffers = new List<JobOffer>();
            WorkerServices = new List<WorkerService>();
        }

        public int Id { get; set; }
        public double RadiusKm { get; set; }
        public DateTime AvailbleUntil { get; set; }


     
        public virtual List<JobOffer> WorkerJobOffers { get; set; }
        public virtual List<WorkerService> WorkerServices { get; set; }
    }
}
