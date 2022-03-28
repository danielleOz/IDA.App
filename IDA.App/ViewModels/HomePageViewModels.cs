using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Views;

namespace IDA.App.ViewModels
{
    class HomePageViewModels
    {

        public ICommand newcommand => new Command(OnUpdate);
        public void OnUpdate()
        {
            Page p = new TheMainTabbedPage();
            App.Current.MainPage.Navigation.PushAsync(p);

        }
    }
}
