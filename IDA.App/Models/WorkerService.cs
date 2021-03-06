using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class WorkerService
    {
        public int ServiceId { get; set; }
        public int WorkerId { get; set; }

        public virtual Service Service { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
