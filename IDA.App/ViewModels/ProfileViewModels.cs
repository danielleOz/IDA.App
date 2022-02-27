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
                if (this.current != null && this.current.Worker != null)
                    return this.current.Worker.IsAvailble;
                return false;
            }
        }
        #endregion



        #region AvailbleWorker 

        public ICommand AvailbleWorkerCommand => new Command(AvailbleWorker);


        private async void AvailbleWorker()
        {
            if (current.User.IsWorker)
            {
                ////// Create a custom DateTime for 7/28/1979 at 10:35:05 PM using a
                ////// calendar based on the "en-US" culture, and ticks.
                ////long ticks = new DateTime(1979, 07, 28, 22, 35, 5,
                ////new CultureInfo("en-US", false).Calendar).Ticks;
                ////DateTime dt3 = new DateTime(ticks);

                //// Create a DateTime for the maximum date and time using ticks.
                //DateTime dt1 = new DateTime(DateTime.MaxValue.Ticks);

                //check if worker is availble or not and change it
                if(current.Worker.IsAvailble)
                    current.Worker.IsAvailble = false;
                else
                    current.Worker.IsAvailble = true;


                //update worker work until
                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
                bool success = await IDAAPIProxy.UpdateWorkerAvailbilty(current.Worker);
                if (!success)
                    Console.WriteLine();// to make an alert 

            }

        }

        #endregion

        //#region UpdateCommand
        //public ICommand UpdateCommand => new Command(OnUpdate);
        //public async void OnUpdate()
        //{
        //    Page page = new UpdateUser();
        //    page.Title = "Update";
        //    await App.Current.MainPage.Navigation.PushAsync(page);
        //}
        //#endregion

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
