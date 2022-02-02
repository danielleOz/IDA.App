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
    class ProfileViewModels
    {

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
            User == null;
            // איפוס המשתמש שמחובר והפניה לדף של התחברות

        }
    }
}
