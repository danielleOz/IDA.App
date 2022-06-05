using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Models;
using IDA.App.Services;
using IDA.App.Views;
using System.Collections.ObjectModel;


namespace IDA.App.ViewModels
{
    public class AvailbiltyViewModels : ViewModelBase
    {
        public AvailbiltyViewModels()
        {
            if (current.Worker.AvailbleUntil.Date < DateTime.Now.Date)
            {
                this.Time = new TimeSpan(0);
            }
            else
            {
                this.Time = new TimeSpan(current.Worker.AvailbleUntil.Hour, current.Worker.AvailbleUntil.Minute, 0);
            }
            this.time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, spanTime.Hours, spanTime.Minutes, 0);

            OnPropertyChanged("IsAvailable");
            OnPropertyChanged("Time");

        }


        #region is Availble
        public string IsAvailable
        {
            get
            {
                if (DateTime.Now <= this.time)
                    return "Available";
                else
                    return "Not Available";
            }
        }
        #endregion


        #region is Availble bool
        public bool IsAvailableBool
        {
            get
            {
                if (DateTime.Now <= this.time)
                    return true;
                else
                    return false;
            }
        }
        #endregion


        #region is NOT Availble bool
        public bool IsntAvailableBool
        {
            get
            {
                if (DateTime.Now <= this.time)
                    return false;
                else
                    return true;
            }
        }
        #endregion


        #region time
        private DateTime time;

        private TimeSpan spanTime;
        public TimeSpan Time
        {
            get
            {
                return this.spanTime;
            }
            set
            {
                if (value != this.spanTime)
                    this.spanTime = value;
            }
        }
        #endregion

        #region Change to Availble Worker 

        //public ICommand AvailbleWorkerCommand => new Command(AvailbleWorker);


        private async void AvailbleWorker()
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, spanTime.Hours, spanTime.Minutes, 0);
            if (current.User.IsWorker)
            {
                current.Worker.AvailbleUntil = time;

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
                    OnPropertyChanged("IsAvailable");
                    OnPropertyChanged("Time");
                    OnPropertyChanged("IsAvailableBool");
                    OnPropertyChanged("IsntAvailableBool");
                }

            }

        }

        #endregion

        #region Change to UnAvailble Worker 

        public ICommand UnAvailbleWorkerCommand => new Command(UnAvailbleWorker);


        private async void UnAvailbleWorker()
        {
            if (current.User.IsWorker)
            {
                this.spanTime = new TimeSpan(0);
                time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, spanTime.Hours, spanTime.Minutes, 0);
                current.Worker.AvailbleUntil = time;

                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
                bool success = await IDAAPIProxy.UpdateWorkerAvailbilty(current.Worker);
                if (!success)
                {
                    await App.Current.MainPage.DisplayAlert(" ", "something went wrong, please try again", "ok", FlowDirection.RightToLeft);
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert(" ", "your now set as Unavailable", "ok", FlowDirection.RightToLeft);
                    OnPropertyChanged("IsAvailable");
                    OnPropertyChanged("Time");
                    OnPropertyChanged("IsAvailableBool");
                    OnPropertyChanged("IsntAvailableBool");

                }

            }

        }

        #endregion

        #region Change to Availble Worker 

        public ICommand AvailbleWorkerCommand => new Command(AvailbleWorker);




        #endregion

    }
}
