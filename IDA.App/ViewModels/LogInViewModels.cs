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
        public LogInViewModels()
        {
            EntryEmail = "www@www.com";
            EntryPass = "123456";
        }

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

      

        #region log in

        public ICommand LogInCommand => new Command(LogIn);
        private async void LogIn()
        {
            IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
            User user = await IDAAPIProxy.LoginAsync(EntryEmail, EntryPass);
            if (user != null)
            {
                TheMainTabbedPage theMainTabbedPage = (TheMainTabbedPage)Application.Current.MainPage.Navigation.NavigationStack[0];
                TheMainTabbedPageViewModels mainPageVM = (TheMainTabbedPageViewModels)theMainTabbedPage.BindingContext;
                this.current.User = user;
                //TO DO: Assign worker and customer as needed
                if (user is Worker)
                {

                    this.current.Worker = (Worker)user;
                }

                mainPageVM.LoginUser = user;

                await App.Current.MainPage.DisplayAlert("", "You are logged in now!", "Ok");
                ((App)Application.Current).services = await IDAAPIProxy.GetServices();

                //Page p = new UserPage();
                //await App.Current.MainPage..PushAsync(p);

                HomePageViewModels homePageViewModels = (HomePageViewModels)((theMainTabbedPage).home.BindingContext);
                //if ((homePageViewModels.CounterCorrectAnswers > 0) && (homePageViewModels.CounterCorrectAnswers % 3 == 0))
                // theMainTabbedPage.AddTab((theMainTabbedPage).addQTab);

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Log In failed, please try again", "Ok");
            }
            EntryPass = "";
            EntryEmail = "";
        }
        #endregion


        #region go to register
        public ICommand RegisterCommand => new Command(GoToRegister);
        private  void GoToRegister()
        {
           
            ((TheMainTabbedPage)Application.Current.MainPage).CurrentTab(((TheMainTabbedPage)Application.Current.MainPage).register);
        }
        #endregion
    }
}
