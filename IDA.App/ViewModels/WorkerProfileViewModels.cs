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
using IDA.App.Services;

namespace IDA.App.ViewModels
{
    class WorkerProfileViewModels:ViewModelBase
    {

        public WorkerProfileViewModels()
        {
            
        }

        List<Worker> WorkersList = new List<Worker>();
        public  WorkerProfileViewModels(int workerId, int serviceId)
        {
           
            int id = workerId;
            GetList();
            Worker w = this.WorkersList.Where(a => a.Worker.Id == id).FirstOrDefault();

            if(w != null)
            {
                city = w.City;
                birthDate = w.Birthday;
                fname = w.FirstName;
                lname = w.LastName;
                email = w.Email;
            }

        }

        public async void GetList()
        {
            IDAAPIProxy proxy = IDAAPIProxy.CreateProxy();
             WorkersList = await proxy.GetAvailableWorkers();
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
        private string city;
        public string City
        {
            get => this.city;
            set
            {
                if (value != this.city)
                {
                    this.city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        #endregion

        #region id
        private int id;
        public int Id
        {
            get => this.id;
            set
            {
                if (value != this.id)
                {
                    this.id = value;
                    OnPropertyChanged("Id");
                }
            }
        }


        #endregion

        #region service id
        private int sId;
        public int SId
        {
            get => this.sId;
            set
            {
                if (value != this.sId)
                {
                    this.sId = value;
                    OnPropertyChanged("SId");
                }
            }
        }


        #endregion

        #region email
        private string email;
        public string Email
        {
            get => this.email;
            set
            {
                if (value != this.email)
                {
                    this.email = value;
                    OnPropertyChanged("Email");
                }
            }
        }


        #endregion
 
        #region first name 
        private string fname;
        public string Fname
        {
            get => this.fname;
            set
            {
                if (value != this.fname)
                {
                    this.fname = value;
                    OnPropertyChanged("Fname");
                }
            }
        }


        #endregion

        #region last name

        private string lname;
        public string Lname
        {
            get => this.lname;
            set
            {
                if (value != this.lname)
                {
                    this.lname = value;
                    OnPropertyChanged("Lname");
                }
            }
        }


        #endregion

        #region birthdate
        private DateTime birthDate = DateTime.Now;
        public DateTime BirthDate
        {
            get => this.birthDate.Date;
            set
            {
                if (value != this.birthDate)
                {
                    this.birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }


        #endregion

        #region age
        private string age;
        public string Age
        {
            get => (DateTime.Now.Year - this.birthDate.Year).ToString();

           
        }


        #endregion

        #region submit
        public ICommand SendEmailCommand => new Command(SendMail);
        private async void SendMail()
        {
            
            IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();

                bool isOK = false;

            JobOffer j = new JobOffer
            {

            };

            j.ServiceId = sId;
            j.ChosenWorkerId = id ;
            j.Description = "";
            j.PublishDate = DateTime.Now;
            j.StatusId = 0;
            j.UserId= this.current.User.Id;

            j = await IDAproxy.JobOffer(j);
            if (j != null) 
            {
                this.current.JobOffer = j;
                isOK = true;
            }


           

                if (isOK)
                {

                    await App.Current.MainPage.DisplayAlert("", "your request has been submitted", "Ok");
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("", "failed please try again", "Ok");
                }


        }


    }

    #endregion

    //public async Task SendEmail()
    //{
    //    try
    //    {
    //        string subject = "New Job Offer";
    //        string body = "hi i would like to schedule the job offer with you ";
    //        List<string>  = recipients
    //        var message = new EmailMessage
    //        {
    //            Subject = subject,
    //            Body = body,
    //            To = recipients,
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





}
