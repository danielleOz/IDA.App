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
    class ProfileViewModels : ViewModelBase
    {
        public ProfileViewModels()
        {
            this.time = this.current.Worker.AvailbleUntil;
        }


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


        #region is Availble
        public bool IsAvailble
        {
            get
            {
                if (this.current.Worker != null)
                    return DateTime.Now <= this.time;
                else
                    return false;
            }

        }
        #endregion


        #region time
        private DateTime time = DateTime.Today;
        public TimeSpan Time
        {
            get => time - DateTime.Today;
            set
            {
                this.time = DateTime.Today.Add(value);
                OnPropertyChanged("Time");
            }
        }
        #endregion

        #region available until
        private DateTime d;
        public DateTime D
        {
            get
            {
                return this.current.Worker.AvailbleUntil;
            }
        }
        #endregion

        #region Change to Availble Worker 

        public ICommand AvailbleWorkerCommand => new Command(AvailbleWorker);


        private async void AvailbleWorker()
        {
            if (current.User.IsWorker)
            {
                if (!IsAvailble)
                    current.Worker.AvailbleUntil = time;
                else
                {
                    current.Worker.AvailbleUntil = DateTime.Today;
                }

                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
                bool success = await IDAAPIProxy.UpdateWorkerAvailbilty(current.Worker);
                if (!success)
                {
                    await App.Current.MainPage.DisplayAlert(" ", "something went wrong, please try again", "ok", FlowDirection.RightToLeft);
                    current.Worker.AvailbleUntil = time;
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert(" ", "your now set as available", "ok", FlowDirection.RightToLeft);
                    time = current.Worker.AvailbleUntil;
                    OnPropertyChanged("Time");
                    OnPropertyChanged("IsAvailable");

                }

            }

        }

        #endregion

        #region go to reviews
        public ICommand GoToReviewCommand => new Command(GoToReview);
        private void GoToReview()
        {
            Page NewPage = new Views.Reviews();
            App.Current.MainPage = NewPage;
        }
        #endregion

        #region UpdateCommand
        public ICommand UpdateCommand => new Command(OnUpdate);
        public void OnUpdate()
        {
            Page NewPage = new Views.Update();
            App.Current.MainPage = NewPage;
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
                App theApp = (App)App.Current;
                theApp.User = null;

                Page p = new TheMainTabbedPage();
                p.Title = "login";
                App.Current.MainPage = p;

            }
        }
        #endregion
    }
}
