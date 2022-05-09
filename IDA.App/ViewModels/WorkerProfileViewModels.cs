using IDA.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace IDA.App.ViewModels
{
    class WorkerProfileViewModels:ViewModelBase
    {

        public WorkerProfileViewModels()
        {
            //User currentUser = ; //the worker from the job offer 
            //entryCity = currentUser.City;
            //entryBirthDate = currentUser.Birthday;
            //entryFname = currentUser.FirstName;
            //entryLname = currentUser.LastName;
            //entryEmail = currentUser.Email;
        }

        #region reviews
        private ObservableCollection<JobOffer> jobOffers;
        public ObservableCollection<JobOffer> JobOffers
        {
            get
            {
                return this.jobOffers;
            }
            set
            {
                this.jobOffers = value;
                OnPropertyChanged("JobOffers");
            }
        }


        #endregion


        #region city
        private string entryCity;
        public string EntryCity
        {
            get => this.entryCity;
            set
            {
                if (value != this.entryCity)
                {
                    this.entryCity = value;
                    OnPropertyChanged("EntryCity");
                }
            }
        }


        #endregion

 
        #region email
        private string entryEmail;
        public string EntryEmail
        {
            get => this.entryEmail;
            set
            {
                if (value != this.entryEmail)
                {
                    this.entryEmail = value;
                    OnPropertyChanged("EntryEmail");
                }
            }
        }


        #endregion

      
        #region first name 
        private string entryFname;
        public string EntryFname
        {
            get => this.entryFname;
            set
            {
                if (value != this.entryFname)
                {
                    this.entryFname = value;
                    OnPropertyChanged("EntryFname");
                }
            }
        }


        #endregion


        #region last name

        private string entryLname;
        public string EntryLname
        {
            get => this.entryLname;
            set
            {
                if (value != this.entryLname)
                {
                    this.entryLname = value;
                    OnPropertyChanged("EntryLname");
                }
            }
        }


        #endregion


        #region birthdate
        private DateTime entryBirthDate = DateTime.Now;
        public DateTime EntryBirthDate
        {
            get => this.entryBirthDate.Date;
            set
            {
                if (value != this.entryBirthDate)
                {
                    this.entryBirthDate = value;
                    OnPropertyChanged("EntryBirthDate");
                }
            }
        }


        #endregion

        #region get age
        private string age;
        public string Age
        {
            get => (DateTime.Now.Year - this.entryBirthDate.Year).ToString();

           
        }


        #endregion

        #region submit
        public ICommand SendEmailCommand => new Command(SendMail);
        private void SendMail()
        {
            JobOffer j = new JobOffer();
            j.ServiceId;
            j.ChosenWorker;
            j.ChosenWorkerId;
            j.Description = "";
            j.PublishDate = DateTime.Now;
            j.StatusId = 0;
            j.UserId;
            this.current.JobOffer = j;
            


        }
        //public async Task SendEmail()
        //{
        //    try
        //    {
        //        string subject = "New Job Offer";
        //        string body = "hi i would like to schedule the job offer with you ";
        //        // List<string> recipients = the workers email ;
        //        var message = new EmailMessage
        //        {
        //            //Subject = subject,
        //            //Body = body,
        //            //To = recipients,
        //        };
        //        await Email.ComposeAsync(message);
        //    }
        //    catch (FeatureNotSupportedException fbsEx)
        //    {
        //        // Email is not supported on this device
        //    }
        //    catch (Exception ex)
        //    {
        //        // Some other exception occurred
        //    }
        //}


        #endregion

    }
}
