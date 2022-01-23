using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class JobOffer
    {
        public JobOffer()
        {
            Reviews = new List<Review>();
        }

        public int Jid { get; set; }
        public int Wid { get; set; }
        public int Cid { get; set; }
        public int Swid { get; set; }
        public int Time { get; set; }
        public int Status { get; set; }
        public int Rid { get; set; }

        public virtual Customers CidNavigation { get; set; }
        public virtual Review RidNavigation { get; set; }
        public virtual Status StatusNavigation { get; set; }
        public virtual WorkerService Sw { get; set; }
        public virtual Workers WidNavigation { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
