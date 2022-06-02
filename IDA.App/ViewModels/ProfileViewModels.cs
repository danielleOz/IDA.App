using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Models;
using IDA.App.Services;
using IDA.App.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace IDA.App.ViewModels
{
   public class ProfileViewModels : ViewModelBase
    {
        public ProfileViewModels()
        {
            User currentUser = this.current.User;
            entryAp = currentUser.Apartment;
            entryCity = currentUser.City;
            entryStreet = currentUser.Street;
            entryHN = currentUser.HouseNumber;
            entryBirthDate = currentUser.Birthday;
            entryFname = currentUser.FirstName;
            entryLname = currentUser.LastName;
            entryPass = currentUser.UserPswd;
            entryEmail = currentUser.Email;
            if(currentUser.IsWorker)
            {
                Worker currentWorker = this.current.Worker;
                double d = currentWorker.RadiusKm;
                entryRadius = d.ToString();
            }
        }

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

        #region Street
        private string entryStreet;
        public string EntryStreet
        {
            get => this.entryStreet;
            set
            {
                if (value != this.entryStreet)
                {
                    this.entryStreet = value;
                    OnPropertyChanged("EntryStreet");
                }
            }
        }


  
        #endregion


        #region ap
        private string entryAp;
        public string EntryAp
        {
            get => this.entryAp;
            set
            {
                if (value != this.entryAp)
                {
                    this.entryAp = value;
                    OnPropertyChanged("EntryAp");
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

        #region house num
        private string entryHN;
        public string EntryHN
        {
            get => this.entryHN;
            set
            {
                if (value != this.entryHN)
                {
                    this.entryHN = value;
                    OnPropertyChanged("EntryHN");
                }
            }
        }

        
        #endregion


        #region password
        private string entryPass;
        public string EntryPass
        {
            get => this.entryPass;
            set
            {
                if (value != this.entryPass)
                {
                    this.entryPass = value;
                    OnPropertyChanged("EntryPass");
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
        private DateTime entryBirthDate = DateTime.Now ;
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


        #region Radius
        private string entryRadius;
        public string EntryRadius
        {
            get => this.entryRadius;
            set
            {
                if (value != this.entryRadius)
                {
                    this.entryRadius = value;
                    OnPropertyChanged("EntryRadius");
                }
            }
        }



        #endregion


        #region is worker
        public bool IsWorker
        {
            get
            {
                if (this.current != null && this.current.User != null)
                    return this.current.User.IsWorker;
                return false;
            }
        }
        #endregion

        //#region available until
        //private DateTime d;
        //public DateTime D
        //{
        //    get
        //    {
        //        return this.current.Worker.AvailbleUntil;
        //    }
        //}
        //#endregion



        //#region UnAvailble Worker Command
        //public ICommand UnAvailbleWorkerCommand => new Command(UnAvailbleWorker);

        //private async void UnAvailbleWorker()
        //{
        //    if (current.User.IsWorker)
        //    {
        //        if (!IsAvailble)
        //            current.Worker.AvailbleUntil = time;
        //        else
        //        {
        //            current.Worker.AvailbleUntil = DateTime.Today;
        //        }

        //        IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
        //        bool success = await IDAAPIProxy.UpdateWorkerAvailbilty(current.Worker);
        //        if (!success)
        //        {
        //            await App.Current.MainPage.DisplayAlert(" ", "something went wrong, please try again", "ok", FlowDirection.RightToLeft);
        //            current.Worker.AvailbleUntil = time;
        //        }

        //        else
        //        {
        //            await App.Current.MainPage.DisplayAlert(" ", "your now set as available", "ok", FlowDirection.RightToLeft);
        //            time = current.Worker.AvailbleUntil;
        //            OnPropertyChanged("Time");
        //            OnPropertyChanged("IsAvailable");

        //        }

        //    }

        //}
        //#endregion

        #region go to reviews
        public ICommand GoToReviewCommand => new Command(GoToReview);
        private void GoToReview()
        {
            List<JobOffer> jobOffers;
            if (IsWorker)
            {
                jobOffers = this.current.Worker.WorkerJobOffers;
            }
            else
            {
                jobOffers = this.current.User.JobOffers;
            }

            ReviewsViewModels vm = new ReviewsViewModels(jobOffers);
            Page NewPage = new Views.Reviews();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);
        }
        #endregion

        #region go to Job offer history page
        public ICommand GoJOHistoryCommand => new Command(JOHistory);
        private void JOHistory()
        {
            List<JobOffer> jobOffers;
            if (IsWorker)
            {
                jobOffers = this.current.Worker.WorkerJobOffers;
            }
            else
            { 
                jobOffers = this.current.User.JobOffers;
            }

            JobHistoryViewModels vm = new JobHistoryViewModels(jobOffers);
            Page NewPage = new Views.JobHistory();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);
        }
        #endregion

        //#region go to job offer
        //public ICommand GoToJobCommand => new Command(JobOffer);
        //private void JobOffer()
        //{
        //    List<JobOffer> jobOffers;
        //    if (IsWorker)
        //    {
        //        jobOffers = this.current.Worker.WorkerJobOffers;
        //    }
        //    else
        //    {
        //        jobOffers = this.current.User.JobOffers;
        //    }

        //    JobOfferPageViewModels vm = new JobOfferPageViewModels(jobOffers);
        //    Page NewPage = new Views.JobOfferPage();
        //    NewPage.BindingContext = vm;
        //    App.Current.MainPage.Navigation.PushAsync(NewPage);
        //}
        //#endregion

        #region go to update page
        public ICommand UpdateCommand => new Command(OnUpdate);
        public void OnUpdate()
        {
            UpdateViewModels vm = new UpdateViewModels();
            Page NewPage = new Views.Update();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);
              
        }
        #endregion

        #region go to updateAvailbilty page

        public ICommand UpdateAvailbiltyCommand => new Command(UpdateAvailbilty);
        public void UpdateAvailbilty()
        {
            Page NewPage = new Views.Availbilty();
            App.Current.MainPage.Navigation.PushAsync(NewPage);
        }
        #endregion

        #region LogOut
        private Command logOutCommand;
        public ICommand LogOutCommand
        {
            get
            {
                if (logOutCommand == null)
                {
                    logOutCommand = new Command(LogOut);
                }

                return logOutCommand;
            }
        }


        private async void LogOut()
        {
            bool answer = await App.Current.MainPage.DisplayAlert("logout", "are you sure you want to logout?", "logout", "cancel", FlowDirection.LeftToRight);
            if (answer)
            {
                current.User = null;
                current.Worker = null;
                Page p = new Views.LogIn();
                App.Current.MainPage = p;
            }
        }
        #endregion



    }
}
