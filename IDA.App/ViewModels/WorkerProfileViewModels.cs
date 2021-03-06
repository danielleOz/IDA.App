using IDA.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Net;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Services;
using IDA.DTO;

namespace IDA.App.ViewModels
{
    public class WorkerProfileViewModels : ViewModelBase
    {

        public WorkerProfileViewModels()
        {


        }

        List<Worker> WorkersList = new List<Worker>();
        public WorkerProfileViewModels(int workerId, int serviceId)
        {
            Id = workerId;
            SId = serviceId;
        }

        public async Task GetList()
        {
            IDAAPIProxy proxy = IDAAPIProxy.CreateProxy();
            Worker w = await proxy.GetWorkerAsync(Id);
         
            Service s = w.WorkerServices.Where(b => b.Service.Id== SId).FirstOrDefault().Service;
            
            List<JobOffer> jobOffers = w.JobOffers.Where(d => d.Service.Id == s.Id).Where(r=> r.WorkerReviewDate != null).ToList();
            ThisWorker = w;
            if (w != null)
            {
                Sname = s.Name;
                City = w.City;
                Age = (DateTime.Now.Year - w.Birthday.Year).ToString();
                Fname = w.FirstName;
                Lname = w.LastName;
                Email = w.Email;
            }
            this.JobOffers = new ObservableCollection<JobOffer>(jobOffers);
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

        #region Thisworker
        private Worker thisWorker;
        public Worker ThisWorker
        {
            get => this.thisWorker;
            set
            {
                if (value != this.thisWorker)
                {
                    this.thisWorker = value;
                    OnPropertyChanged("ThisWorker");
                }
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

        #region service name 
        private string sname;
        public string Sname
        {
            get => this.sname;
            set
            {
                if (value != this.sname)
                {
                    this.sname = value;
                    OnPropertyChanged("Sname");
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
        private DateTime birthDate;
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
            get => this.age;
            set
            {
                if (value != this.age)
                {
                    this.age = value;
                    OnPropertyChanged("Age");
                }
            }
        }


        #endregion

        //#region reviews
        //private ObservableCollection<JobOffer> jobOffers;
        //public ObservableCollection<JobOffer> JobOffers
        //{
        //    get
        //    {
        //        return this.jobOffers;
        //    }
        //    set
        //    {
        //        this.jobOffers = value;
        //        OnPropertyChanged("JobOffers");
        //    }
        //}


        //#endregion


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
            j.ChosenWorkerId = id;
            j.Description = "";
            j.PublishDate = DateTime.Now;
            j.StatusId = 0;
            j.UserId = this.current.User.Id;

            j = await IDAproxy.JobOffer(j);
            if (j != null)
            {
                this.current.JobOffer = j;
                isOK = true;
            }

            if (isOK)
            {
                try
                {
                     bool s = await SendEmail();
                    if(s)
                        await App.Current.MainPage.DisplayAlert("", "your request has been submitted", "Ok");
                    else
                        await App.Current.MainPage.DisplayAlert("", "your request could not be Sent", "Ok");
                }
                catch
                {
                    await App.Current.MainPage.DisplayAlert("", "your request could not be Sent", "Ok");
                }
               
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("", "failed please try again", "Ok");
            }


        }
        public async Task<bool> SendEmail()
        {
            try
            {
                IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();
               bool sucsess = await IDAproxy.SendMail(thisWorker);
                return sucsess;
            }

            catch (Exception ex)
            {
                throw new Exception("error sending email");
                return false;
            }
        }

        #endregion




    }
}


