using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Service
    {
        public Service()
        {
            WorkerServices = new List<WorkerService>();
        }

        public int Sid { get; set; }
        public string Name { get; set; }

        public virtual List<WorkerService> WorkerServices { get; set; }
    }
}
