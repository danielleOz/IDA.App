using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Service
    {
        public Service()
        {
            JobOffers = new List<JobOffer>();
            WorkerServices = new List<WorkerService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<JobOffer> JobOffers { get; set; }
        public virtual List<WorkerService> WorkerServices { get; set; }
    }
}
