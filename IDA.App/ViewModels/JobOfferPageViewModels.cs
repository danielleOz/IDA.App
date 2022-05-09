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
using System.Threading.Tasks;
using Xamarin.Essentials;
using Location = Xamarin.Essentials.Location;

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
            SelectServicesCommand = new Command<string>(SelectService);
        }

       

        #region is worker
        public bool IsWorker
        {
            get
            {
                if (this.current != null && this.current.User != null)
                    return this.current.User.IsWorker;
                return false;
            }
        }
        #endregion

        #region go to update Availbilty page

        public ICommand UpdateAvailbiltyCommand => new Command(UpdateAvailbilty);
        public void UpdateAvailbilty()
        {
            Page NewPage = new Views.Availbilty();
            App.Current.MainPage.Navigation.PushAsync(NewPage);
        }
        #endregion

        //#region isnt worker

        //public bool IsntWorker
        //{
        //    get
        //    {
        //        if (this.current != null && this.current.User != null)
        //            return this.current.User.IsWorker;
        //        return false;
        //    }
        //}
        //#endregion

        #region services
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

        private string selectedService;
        public string SelectedService
        {
            get => selectedService;
            set
            {
                selectedService = value;
                OnPropertyChanged("SelectedService");
            }
        }

        private Service selected;
        public ICommand SelectServicesCommand { get; set; }

        private void SelectService(string selected)
        {
            if (this.SelectedService != null)
                this.selected = this.allServices.Where(sw => sw.Name == this.SelectedService).FirstOrDefault();
            else
                this.selected = null;


        }

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

        #region Is Services Enabled
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

        #region On Services Changed
        public void OnServicesChanged(string search)
        {
            if (this.services != this.SelectedServicesItem)
            {
                this.ShowServices = true;
                this.SelectedServicesItem = null;
            }
           
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
            WorkerProfileViewModels vm = new WorkerProfileViewModels(workerId, selected.Id);
            Page NewPage = new Views.WorkerProfile();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);

        }
        #endregion

        #region workers list
        public ICommand SearchCommand => new Command(search);
        public async void search()
        {
            IDAAPIProxy proxy = IDAAPIProxy.CreateProxy();

            List<Worker> l = await proxy.GetAvailableWorkers();

            if (l != null)
            {
                //string userAddress = current.User.Street + " " + current.User.HouseNumber +" " + current.User.City + " " + "ISRAEL";
                //var userLocations = await Geocoding.GetLocationsAsync(userAddress);
                //var userLocation = userLocations?.FirstOrDefault();
                //List<Worker> closeToMe = new List<Worker>();
                //foreach(Worker w in l)
                //{
                //    var workerAddress = $" {w.Street} {w.HouseNumber} {w.City} ISRAEL";
                //    var workerLocations = await Geocoding.GetLocationsAsync(workerAddress);
                //    var workerlocation = workerLocations?.FirstOrDefault();
                //    if(workerlocation!=null)
                //    {
                //        var distance = Location.CalculateDistance(userLocation, workerlocation, DistanceUnits.Kilometers);
                //        if (distance <= w.RadiusKm)
                //            closeToMe.Add(w);
                //    }

                //}
                this.Workers = new ObservableCollection<Worker>(/*closeToMe*/ l);
            }
            else this.Workers = new ObservableCollection<Worker>();

            if (string.IsNullOrEmpty(Services))
                selected = null;
 
            FilterList(l);


        }

        private ObservableCollection<Worker> workers;
        public ObservableCollection<Worker> Workers
        {
            get
            {
                return this.workers;
            }
            set
            {
                this.workers = value;
                OnPropertyChanged("Workers");
            }
        }

        private async Task<List<JobOffer>> JobOffer()
        {
            //List<JobOffer> jobOffers;
            //if (IsWorker)
            //{
            //   return jobOffers = this.current.Worker.WorkerJobOffers;
            //}
            //else
            //{
            //   return jobOffers = this.current.User.JobOffers;
            //}
            return await IDAproxy.GetWorkerReviews();
        }

        public void FilterList(List<Worker> workers)
        {
            if (selected == null)
                this.Workers.Clear() ;

            else
            {
                List<Worker> list = this.workers.Where(w => w.WorkerServices.Where(s => s.Service.Name == selected.Name).FirstOrDefault() != null).ToList();
                this.Workers = new ObservableCollection<Worker>(list);
            }

        }

        private string workerId;
        public string WorkerId
        {
            get => workerId;
            set
            {
                workerId = value;
                OnPropertyChanged("WorkerId");
            }
        }
        #endregion

    }
}
