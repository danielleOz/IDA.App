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
    class AvailbiltyViewModels:ViewModelBase
    {
        public AvailbiltyViewModels()
        {

            this.time = this.current.Worker.AvailbleUntil;

        }


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

    }
}
