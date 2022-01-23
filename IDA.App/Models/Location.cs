using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class Location
    {
        public Location()
        {
            Customers = new List<Customers>();
            Workers = new List<Workers>();
        }

        public int Lid { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }

        public virtual List<Customers> Customers { get; set; }
        public virtual List<Workers> Workers { get; set; }
    }
}
