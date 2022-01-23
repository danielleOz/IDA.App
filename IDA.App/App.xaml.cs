using System;
using Xamarin.Forms;
using IDA.App.ViewModels;
using IDA.App.Views;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using IDA.App.Models;

namespace IDA.App
{
    public partial class App : Application
    {
        
        public User User
        { get;set; }

        public Customers Customer
        { get; set; }

        public Workers Worker
        { get; set; }

        public List<Service> services { get; set; }
        public static bool IsDevEnv
        {
            get
            {
                return true; //change this before release!
            }
        }
        public App()
        {
            services = new List<Service>();
            InitializeComponent();

            MainPage = new TheMainTabbedPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
