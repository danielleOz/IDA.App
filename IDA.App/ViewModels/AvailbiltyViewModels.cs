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

            this.Time = this.current.Worker.AvailbleUntil - DateTime.Today;

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

        #region time
        private DateTime time = DateTime.Today;
        public TimeSpan Time
        {
            get => time - DateTime.Today;
            set
            {
                this.time = DateTime.Today.Add(value);
                OnPropertyChanged("IsAvailable");
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
                    time = current.Worker.AvailbleUntil;
                    Time = time - DateTime.Today;

                }

            }

        }

        #endregion

    }
}
