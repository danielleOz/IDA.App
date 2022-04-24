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
using System.Linq;

namespace IDA.App.ViewModels
{
    class JobOfferPageViewModels : ViewModelBase
    {
        private List<Service> allServices;
        private ObservableCollection<string> filteredServices;
        public ObservableCollection<string> FilteredServices
        {
            get
            {
                return this.filteredServices;
            }
            set
            {
                if (this.filteredServices != value)
                {

                    this.filteredServices = value;
                    OnPropertyChanged("FilteredServices");
                }
            }
        }

        public JobOfferPageViewModels()
        {
            this.allServices = current.services;
            this.FilteredServices = new ObservableCollection<string>();
        }


        #region is worker
        private bool isWorker;
        public bool IsWorker
        {
            get => current.User.IsWorker;
            set
            {
                if (current.User.IsWorker)
                {
                    isWorker = value;
                    OnPropertyChanged("IsWorker");
                }

            }
        }
        #endregion


        #region isnt worker
        private bool isntWorker;
        public bool IsntWorker
        {
            get => this.current.User.IsWorker;
            set
            {
                if (!this.current.User.IsWorker)
                {
                    this.isntWorker = value;
                    OnPropertyChanged("IsntWorker");
                }

            }
        }
        #endregion

        #region service
        //private bool showStreetError;
        //public bool ShowStreetError
        //{
        //    get => showStreetError;
        //    set
        //    {
        //        showStreetError = value;
        //        OnPropertyChanged("ShowStreetError");
        //    }
        //}

        //This property holds the selected street on the collection of streets

        private string selectedServicesItem;
        public string SelectedServicesItem
        {
            get => selectedServicesItem;
            set
            {
                selectedServicesItem = value;
                OnPropertyChanged("SelectedServicesItem");
            }
        }

        private Service selected;
        public ICommand SelectServicesCommand { get; set; }

        //private void SelectService()
        //{
        //    if (selected == null)
        //        selected = new Service();
        //    selected = selectedServicesItem;
        //    Service s = this.se.Where(s => s.street_name == this.Street).FirstOrDefault();

        //}

        //ShowServices
        private bool showServices;
        public bool ShowServices
        {
            get => showServices;
            set
            {
                showServices = value;
                OnPropertyChanged("ShowServices");
            }
        }

        private string services;
        public string Services
        {
            get => services;
            set
            {
                services = value;
                OnServicesChanged(value);
                //ValidateStreet();
                OnPropertyChanged("Services");
            }
        }


        //private string streetError;
        //public string StreetError
        //{
        //    get => streetError;
        //    set
        //    {
        //        streetError = value;
        //        OnPropertyChanged("StreetError");
        //    }
        //}

        //private void ValidateStreet()
        //{
        //    this.ShowStreetError = string.IsNullOrEmpty(this.Street);
        //    if (!this.ShowStreetError)
        //    {
        //        Street street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
        //        if (street == null)
        //        {
        //            this.ShowStreetError = true;
        //            this.StreetError = ERROR_MESSAGES.BAD_STREET;
        //        }
        //    }
        //    else
        //        this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        //}
        #endregion

        #region IsServicesEnabled
        private bool isServicesEnabled;
        public bool IsServicesEnabled
        {
            get => isServicesEnabled;
            set
            {
                isServicesEnabled = value;
                OnPropertyChanged("IsServicesEnabled");
            }
        }
        #endregion

        #region OnServicesChanged
        public void OnServicesChanged(string search)
        {
            if (this.services != this.SelectedServicesItem)
            {
                this.ShowServices = true;
                this.SelectedServicesItem = null;
            }
            //Filter the list of streets based on the search term
            if (this.allServices == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowServices = false;
                this.FilteredServices.Clear();
            }
            else
            {
                foreach (Service s in this.allServices)
                {
                    string ServiceName = s.Name;

                    if (!this.FilteredServices.Contains(ServiceName) && ServiceName.Contains(search))
                        this.FilteredServices.Add(ServiceName);
                    else if (this.FilteredServices.Contains(ServiceName) && (!ServiceName.Contains(search)))
                        this.FilteredServices.Remove(ServiceName);
                }
            }
        }
        #endregion

        #region go to worker profile page
        public ICommand WorkerPCommand => new Command(workerP);
        public void workerP()
        {
            WorkerProfileViewModels vm = new WorkerProfileViewModels();
            Page NewPage = new Views.WorkerProfile();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);

        }
        #endregion


        private ObservableCollection<JobOffer> jobOffers;
        public ObservableCollection<JobOffer> JobOffers
        {
            get
            {
                return this.jobOffers;
            }
            set
            {
                this.jobOffers = value;
                OnPropertyChanged("JobOffers");
            }
        }



        public JobOfferPageViewModels(List<JobOffer> jobOffers)
        {
            List<JobOffer> filtered = jobOffers.Where(j => j.Service.Name == selectedServicesItem).ToList();
            this.jobOffers = new ObservableCollection<JobOffer>(filtered);
        }

    }
}
