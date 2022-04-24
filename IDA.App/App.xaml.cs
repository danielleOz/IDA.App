using System;
using Xamarin.Forms;
using IDA.App.ViewModels;
using IDA.App.Views;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using IDA.App.Models;
using IDA.DTO;
using IDA.App.Services;

namespace IDA.App
{
    public partial class App : Application
    {
        
        public User User
        { get;set; }



        public Worker Worker
        { get; set; }

        public List<Service> services { get; set; }
        public static bool IsDevEnv
        {
            get
            {
                return true; //change this before release!
            }
        }

        public List<string> Cities { get; set; }
        public List<string> Streets { get; set; }
        public List<Street> StreetList { get; set; }


        public App()
        {
            Cities = new List<string>();
            Streets = new List<string>();
            StreetList = new List<Street>();
            services = new List<Service>();
            //OnStart();
            InitializeComponent();
            MainPage = new Loading();
            //TheMainTabbedPageViewModels vm = new TheMainTabbedPageViewModels();
            //TheMainTabbedPage tabbedPage = new TheMainTabbedPage();
            //tabbedPage.BindingContext = vm;

            Page page = new HomePage();
            MainPage = new NavigationPage(page);
            //MainPage = new NavigationPage(tabbedPage)
            //{
            //    BarBackgroundColor = Color.FromHex("#f0d9d7")
            //};

        }

        public TheMainTabbedPage TheMainTabbedPage { get; private set; }
        protected async override void OnStart()
        {
           
            IDAAPIProxy proxy = IDAAPIProxy.CreateProxy();
            this.Streets = await proxy.GetStreetsAsync();
            this.Cities = await proxy.GetCitiesAsync();
            this.StreetList = await proxy.GetStreetListAsync();
            this.services = await proxy.GetServices();
            //Page page = new JobOfferPage();
            //MainPage = new NavigationPage(page);
            TheMainTabbedPageViewModels vm = new TheMainTabbedPageViewModels();
            TheMainTabbedPage tabbedPage = new TheMainTabbedPage();
            TheMainTabbedPage = tabbedPage;
            tabbedPage.BindingContext = vm;
            MainPage = new NavigationPage(tabbedPage)
            {
                BarBackgroundColor = Color.FromHex("#f0d9d7")
            };


        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
