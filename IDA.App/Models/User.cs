using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class User
    {
        public User()
        {
            ChatMessageRecievers = new List<ChatMessage>();
            ChatMessageSenders = new List<ChatMessage>();
            JobOffers = new List<JobOffer>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserPswd { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public string HouseNumber { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsWorker { get; set; }

        public virtual List<ChatMessage> ChatMessageRecievers { get; set; }
        public virtual List<ChatMessage> ChatMessageSenders { get; set; }
        public virtual List<JobOffer> JobOffers { get; set; }
    }
}
