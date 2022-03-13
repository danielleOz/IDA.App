using System;
using System.Collections.Generic;
using System.Text;

namespace IDA.App.Models
{
    public partial class JobOffer
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int? ChosenWorkerId { get; set; }
        public int UserId { get; set; }
        public DateTime PublishDate { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public string WorkerReviewDescriptipon { get; set; }
        public int? WorkerReviewRate { get; set; }
        public DateTime? WorkerReviewDate { get; set; }

        public virtual Worker ChosenWorker { get; set; }
        public virtual Service Service { get; set; }
        public virtual JobOfferStatus Status { get; set; }
        public virtual User User { get; set; }

        //The following properties were added to the app only
        public string ReviewDateString
        {
            get
            {
                if (WorkerReviewDate == null)
                    return "";

                TimeSpan? span = DateTime.Now - WorkerReviewDate;
                if (span != null)
                {
                    if (span.Value.Days >= 365)
                        return $"{span.Value.Days/365} Years ago";
                    if (span.Value.Days >= 30)
                        return $"{span.Value.Days / 30} Months ago";
                    if (span.Value.Days >= 1)
                        return $"{span.Value.Days} Days ago";
                    if (span.Value.Hours >= 1)
                        return $"{span.Value.Hours} Hours ago";
                    if (span.Value.Minutes >= 1)
                        return $"{span.Value.Minutes} Minutes ago";
                    return "Just now!";
                }
                return "";

            }
        }
    }
}
