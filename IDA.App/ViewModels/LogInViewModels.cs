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
    class LogInViewModels : ViewModelBase
    {
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

        public LogInViewModels()
        {

        }

        public ICommand LogInCommand => new Command(LogIn);
        private async void LogIn()
        {
            IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
            User user = await IDAAPIProxy.LoginAsync(EntryEmail, EntryPass);
            if (user != null)
            {
                TheMainTabbedPage theMainTabbedPage = (TheMainTabbedPage)Application.Current.MainPage;
                TheMainTabbedPageViewModels mainPageVM = (TheMainTabbedPageViewModels)theMainTabbedPage.BindingContext;
                mainPageVM.LoginUser = user;
                this.current.User = user;
                //TO DO: Assign worker and customer as needed
                //if (user.IsWorker)
                //{
                    
                //    this.current.Worker = 
                //}
              
                await App.Current.MainPage.DisplayAlert("IDA", "You are logged in now!", "Ok");
                ((App)Application.Current).services = await IDAAPIProxy.GetServices();

                //Page p = new UserPage();
                //await App.Current.MainPage..PushAsync(p);

                HomePageViewModels homePageViewModels = (HomePageViewModels)((theMainTabbedPage).home.BindingContext);
                //if ((homePageViewModels.CounterCorrectAnswers > 0) && (homePageViewModels.CounterCorrectAnswers % 3 == 0))
                //    theMainTabbedPage.AddTab((theMainTabbedPage).addQTab);

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("IDA", "Log In failed, please try again", "Ok");
            }
            EntryPass = "";
            EntryEmail = "";
        }

        public ICommand RegisterCommand => new Command(GoToRegister);
        private async void GoToRegister()
        {
           
            ((TheMainTabbedPage)Application.Current.MainPage).CurrentTab(((TheMainTabbedPage)Application.Current.MainPage).register);
        }
    }
}
