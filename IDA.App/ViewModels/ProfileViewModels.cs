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

        private Command availbleCommand;
        public ICommand AvailbleCommand
        {
            get
            {
                if (availbleCommand == null)
                {
                    availbleCommand = new Command(AvailbleWorker);
                }

                return availbleCommand;
            }

        }

        private async void AvailbleWorker()
        {
            bool Availble = 
        }



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
    }
}
