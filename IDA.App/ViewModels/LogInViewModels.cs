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
        // פעולה בונה
        public LogInViewModels()
        {
            EntryEmail = "danielle.oz.do@gmail.com";
            EntryPass = "123456";
        }

        // שדה של הכנסת אימייל
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

        // שדה של הכנסת סיסמא
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
        // פעולת התחברות - בודקת בנוסף עם המשתמש הוא עובד או לקוח
        public ICommand LogInCommand => new Command(LogIn);
        private async void LogIn()
        {
            IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
            User user = await IDAAPIProxy.LoginAsync(EntryEmail, EntryPass);
            if (user is Worker)
            {

                this.current.Worker = (Worker)user;
            }
            if (user != null)
            {
                this.current.User = user;

                TheMainTabbedPage theMainTabbedPage= new Views.TheMainTabbedPage(); ;
                
                TheMainTabbedPageViewModels mainPageVM = new TheMainTabbedPageViewModels();
                theMainTabbedPage.BindingContext = mainPageVM;

                await App.Current.MainPage.DisplayAlert("", "You are logged in now!", "Ok");
                ((App)Application.Current).services = await IDAAPIProxy.GetServices();

                App.Current.MainPage = new NavigationPage(theMainTabbedPage)
                {
                    BarBackgroundColor = Color.FromHex("#B08968")

                };
            ;
                //Page p = new UserPage();
                //await App.Current.MainPage..PushAsync(p);

                //JobOfferPageViewModels JobOfferViewModels = (JobOfferPageViewModels)(theMainTabbedPage.home.BindingContext);

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
        // פעולת מעבר לעמוד של הרשמה
        public ICommand RegisterCommand => new Command(GoToRegister);
        private  void GoToRegister()
        {

            Page NewPage = new Views.Register();
            App.Current.MainPage.Navigation.PushAsync(NewPage);

        }
        #endregion
    }
}
