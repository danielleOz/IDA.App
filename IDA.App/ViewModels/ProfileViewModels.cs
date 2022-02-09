using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Models;
using IDA.App.Services;
using IDA.App.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace IDA.App.ViewModels
{
    class ProfileViewModels:ViewModelBase
    {

        #region is worker
        private bool isWorker;
        public bool IsWorker
        {
            get => this.isWorker;
            set
            {
                if (this.current.User.IsWorker)
                {
                    this.isWorker = value;
                    OnPropertyChanged("IsWorker");
                }

            }
        }
        #endregion


        #region AvailbleWorker 

        public ICommand AvailbleWorkerCommand => new Command(AvailbleWorker);


        private async void AvailbleWorker()
        {
            if(current.User.IsWorker)
            {
                current.Worker.Availble = true;
               //update worker work until
                
                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
              bool success= await IDAAPIProxy.WorkerAvailbilty(current.Worker);
                if(!success)
                    Console.WriteLine();// alert 


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
            if(answer)
            {
                App theApp = (App)App.Current;
                theApp.User = null;

                Page p = new TheMainTabbedPage();
                p.Title = "login";
                App.Current.MainPage =p;

            }
        }
        #endregion
    }
}
