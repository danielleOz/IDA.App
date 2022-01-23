using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class User
    {
        public User()
        {
            Customers = new List<Customers>();
            Workers = new List<Workers>();
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPswd { get; set; }
        public string Adress { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsWorker { get; set; }

        public virtual List<Customers> Customers { get; set; }
        public virtual List<Workers> Workers { get; set; }
    }
}
