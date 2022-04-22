using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using IDA.App.Models;
using IDA.App.Services;
using IDA.App.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IDA.DTO;
using System.Linq;

namespace IDA.App.ViewModels
{

    class UpdateViewModels : ViewModelBase
    {

        //public ObservableCollection<object> selectedServices;
        //public ObservableCollection<object> SelectedServices
        //{
        //    get => selectedServices;
        //    set
        //    {
        //        if (selectedServices != value)
        //            selectedServices = value;
        //    }
        //}
        //private List<Service> workerServices;

        //private ObservableCollection<Service> services;
        //public ObservableCollection<Service> Services { get => services; set { services = value; OnPropertyChanged("Services"); } }

        //public static class ERROR_MESSAGES
        //{
        //    public const string REQUIRED_FIELD = "this is a required field";
        //    public const string BAD_EMAIL = "invalid email";
        //    public const string SHORT_PASS = "password must contain at least 6 characters";
        //    public const string BAD_PHONE = "invalid phone";
        //    public const string BAD_DATE = "you must be above 18";
        //    public const string BAD_RADIUS = "must be a number";
        //    public const string BAD_CITY = "city must be from list";
        //    public const string BAD_STREET = "must be from list";
        //}

        public UpdateViewModels()
        {
            App current = ((App)Application.Current);
            IsWorker = false;
            SelectedServices = new ObservableCollection<object>();
            //UpdateCommand = new Command(Update);
            SelectServicesCommand = new Command(SelectServices);
            this.allCities = current.Cities;
            this.FilteredCities = new ObservableCollection<string>();
            this.allStreets = current.StreetList;
            this.FilteredStreets = new ObservableCollection<string>();
            User currentUser = this.current.User;
            entryAp = currentUser.Apartment;
            city = currentUser.City;
            street = currentUser.Street;
            entryHN = currentUser.HouseNumber;
            entryBirthDate = currentUser.Birthday;
            entryFname = currentUser.FirstName;
            entryLname = currentUser.LastName;
            entryPass = currentUser.UserPswd;
            if(currentUser.IsWorker)
            {
                Worker CurruntWorker = this.current.Worker;
                entryRadius = CurruntWorker.RadiusKm.ToString();
            }



        }

        #region city + street

        public ObservableCollection<object> selectedServices;
        public ObservableCollection<object> SelectedServices
        {
            get => selectedServices;
            set
            {
                if (selectedServices != value)
                    selectedServices = value;
            }
        }
        private List<string> allCities;
        private ObservableCollection<string> filteredCities;
        public ObservableCollection<string> FilteredCities
        {
            get
            {
                return this.filteredCities;
            }
            set
            {
                if (this.filteredCities != value)
                {

                    this.filteredCities = value;
                    OnPropertyChanged("FilteredCities");
                }
            }
        }

        private List<Street> allStreets;
        private ObservableCollection<string> filteredStreets;
        public ObservableCollection<string> FilteredStreets
        {
            get
            {
                return this.filteredStreets;
            }
            set
            {
                if (this.filteredStreets != value)
                {

                    this.filteredStreets = value;
                    OnPropertyChanged("FilteredStreets");
                }
            }
        }
        private List<Service> workerServices;

        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Services { get => services; set { services = value; OnPropertyChanged("Services"); } }

        public static class ERROR_MESSAGES
        {
            public const string REQUIRED_FIELD = "this is a required field";
            public const string BAD_EMAIL = "invalid email";
            public const string SHORT_PASS = "password must contain at least 6 characters";
            public const string BAD_PHONE = "invalid phone";
            public const string BAD_DATE = "you must be above 18";
            public const string BAD_RADIUS = "must be a number";
            public const string BAD_CITY = "city must be from list";
            public const string BAD_STREET = "must be from list";
        }
        #endregion

        #region OnCityChanged
        public void OnCityChanged(string search)
        {
            this.Street = "";
            this.ShowStreets = false;
            this.FilteredStreets.Clear();
            this.IsStreetEnabled = false;

            if (this.City != this.SelectedCityItem)
            {
                this.ShowCities = true;
                this.SelectedCityItem = null;
            }
            //Filter the list of cities based on the search term
            if (this.allCities == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowCities = false;
                this.FilteredCities.Clear();
            }
            else
            {
                foreach (string city in this.allCities)
                {
                    if (!this.FilteredCities.Contains(city) &&
                        city.Contains(search))
                        this.FilteredCities.Add(city);
                    else if (this.FilteredCities.Contains(city) &&
                        !city.Contains(search))
                        this.FilteredCities.Remove(city);
                }
            }
        }
        #endregion

        #region IsStreetEnabled
        private bool isStreetEnabled;
        public bool IsStreetEnabled
        {
            get => isStreetEnabled;
            set
            {
                isStreetEnabled = value;
                OnPropertyChanged("IsStreetEnabled");
            }
        }
        #endregion

        #region OnStreetChanged
        public void OnStreetChanged(string search)
        {
            if (this.Street != this.SelectedStreetItem)
            {
                this.ShowStreets = true;
                this.SelectedStreetItem = null;
            }
            //Filter the list of streets based on the search term
            if (this.allStreets == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                this.ShowStreets = false;
                this.FilteredStreets.Clear();
            }
            else
            {
                foreach (Street street in this.allStreets)
                {
                    string streetName = street.street_name;

                    if (!this.FilteredStreets.Contains(streetName) &&
                        streetName.Contains(search) && street.city_name == this.City)
                        this.FilteredStreets.Add(streetName);
                    else if (this.FilteredStreets.Contains(streetName) &&
                        (!streetName.Contains(search) || !(street.city_name == this.City)))
                        this.FilteredStreets.Remove(streetName);
                }
            }
        }
        #endregion

        #region SelectedCity
        public ICommand SelectedCity => new Command<string>(OnSelectedCity);
        public void OnSelectedCity(string city)
        {
            if (city != null)
            {
                this.ShowCities = false;
                this.City = city;

                this.IsStreetEnabled = true;
            }
        }
        #endregion

        #region SelectedStreet
        public ICommand SelectedStreet => new Command<string>(OnSelectedStreet);
        public void OnSelectedStreet(string street)
        {
            if (street != null)
            {
                this.ShowStreets = false;
                this.Street = street;
            }
        }
        #endregion

        #region City
        private bool showCityError;
        public bool ShowCityError
        {
            get => showCityError;
            set
            {
                showCityError = value;
                OnPropertyChanged("ShowCityError");
            }
        }

        //This property holds the selected city on the collection of cities
        private string selectedCityItem;
        public string SelectedCityItem
        {
            get => selectedCityItem;
            set
            {
                selectedCityItem = value;
                OnPropertyChanged("SelectedCityItem");
            }
        }

        //ShowCities
        private bool showCities;
        public bool ShowCities
        {
            get => showCities;
            set
            {
                showCities = value;
                OnPropertyChanged("ShowCities");
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnCityChanged(value);
                ValidateCity();
                OnPropertyChanged("City");
            }
        }

        private string cityError;
        public string CityError
        {
            get => cityError;
            set
            {
                cityError = value;
                OnPropertyChanged("CityError");
            }
        }

        private void ValidateCity()
        {
            this.ShowCityError = string.IsNullOrEmpty(this.City);
            if (!this.ShowCityError)
            {
                string city = this.allCities.Where(c => c == this.City).FirstOrDefault();
                if (string.IsNullOrEmpty(city))
                {
                    this.ShowCityError = true;
                    this.CityError = ERROR_MESSAGES.BAD_CITY;
                }
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region Street
        private bool showStreetError;
        public bool ShowStreetError
        {
            get => showStreetError;
            set
            {
                showStreetError = value;
                OnPropertyChanged("ShowStreetError");
            }
        }

        //This property holds the selected street on the collection of streets
        private string selectedStreetItem;
        public string SelectedStreetItem
        {
            get => selectedStreetItem;
            set
            {
                selectedStreetItem = value;
                OnPropertyChanged("SelectedStreetItem");
            }
        }

        //ShowStreets
        private bool showStreets;
        public bool ShowStreets
        {
            get => showStreets;
            set
            {
                showStreets = value;
                OnPropertyChanged("ShowStreets");
            }
        }

        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnStreetChanged(value);
                ValidateStreet();
                OnPropertyChanged("Street");
            }
        }

        private string streetError;
        public string StreetError
        {
            get => streetError;
            set
            {
                streetError = value;
                OnPropertyChanged("StreetError");
            }
        }

        private void ValidateStreet()
        {
            this.ShowStreetError = string.IsNullOrEmpty(this.Street);
            if (!this.ShowStreetError)
            {
                Street street = this.allStreets.Where(s => s.street_name == this.Street).FirstOrDefault();
                if (street == null)
                {
                    this.ShowStreetError = true;
                    this.StreetError = ERROR_MESSAGES.BAD_STREET;
                }
            }
            else
                this.StreetError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region ap
        private string entryAp;
        public string EntryAp
        {
            get => this.entryAp;
            set
            {
                if (value != this.entryAp)
                {
                    this.entryAp = value;
                    ValidateAp();
                    OnPropertyChanged("EntryAp");
                }
            }
        }

        private bool showApError;
        public bool ShowApError
        {
            get => showApError;
            set
            {
                showApError = value;
                OnPropertyChanged("ShowApError");
            }
        }


        private string apError;
        public string ApError
        {
            get => apError;
            set
            {
                apError = value;
                OnPropertyChanged("ApError");
            }
        }

        private void ValidateAp()
        {
            this.ShowApError = string.IsNullOrEmpty(entryAp);
            if (!this.ShowApError)
            {
                bool isOK = int.TryParse(entryAp, out _);
                if (!isOK)
                {
                    this.ShowApError = true;
                    this.ApError = ERROR_MESSAGES.BAD_RADIUS;
                }
            }
            else
                this.ApError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region house num
        private string entryHN;
        public string EntryHN
        {
            get => this.entryHN;
            set
            {
                if (value != this.entryHN)
                {
                    this.entryHN = value;
                    ValidateHN();
                    OnPropertyChanged("EntryHN");
                }
            }
        }

        private bool showHNError;
        public bool ShowHNError
        {
            get => showHNError;
            set
            {
                showHNError = value;
                OnPropertyChanged("ShowHNError");
            }
        }


        private string hNError;
        public string HNError
        {
            get => hNError;
            set
            {
                hNError = value;
                OnPropertyChanged("HNError");
            }
        }

        private void ValidateHN()
        {
            this.ShowHNError = string.IsNullOrEmpty(entryHN);
            if (!this.ShowHNError)
            {
                bool isOK = int.TryParse(entryHN, out _);
                if (!isOK)
                {
                    this.ShowHNError = true;
                    this.HNError = ERROR_MESSAGES.BAD_RADIUS;
                }
            }
            else
                this.HNError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region password
        private string entryPass;
        public string EntryPass
        {
            get => this.entryPass;
            set
            {
                if (value != this.entryPass)
                {
                    this.entryPass = value;
                    ValidatePassword();
                    OnPropertyChanged("EntryPass");
                }
            }
        }

        private bool showPassErorr;
        public bool ShowPassErorr
        {
            get => showPassErorr;
            set
            {
                showPassErorr = value;
                OnPropertyChanged("ShowPassErorr");
            }
        }


        private string passError;
        public string PassError
        {
            get => passError;
            set
            {
                passError = value;
                OnPropertyChanged("PassError");
            }
        }

        private void ValidatePassword()
        {
            this.ShowPassErorr = string.IsNullOrEmpty(EntryPass);
            if (!this.ShowPassErorr)
            {
                if (this.EntryPass.Length < 6)
                {
                    this.ShowPassErorr = true;
                    this.PassError = ERROR_MESSAGES.SHORT_PASS;
                }
            }
            else
                this.PassError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region first name 
        private string entryFname;
        public string EntryFname
        {
            get => this.entryFname;
            set
            {
                if (value != this.entryFname)
                {
                    this.entryFname = value;
                    ValidateName();
                    OnPropertyChanged("EntryFname");
                }
            }
        }

        private bool showNameError;
        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }


        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(EntryFname);
            if (ShowNameError)
                NameError = ERROR_MESSAGES.REQUIRED_FIELD;
            else
                NameError = string.Empty;

        }

        #endregion


        #region last name

        private string entryLname;
        public string EntryLname
        {
            get => this.entryLname;
            set
            {
                if (value != this.entryLname)
                {
                    this.entryLname = value;
                    ValidateLastName();
                    OnPropertyChanged("EntryLname");
                }
            }
        }

        private bool showLastNameError;
        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;
        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = string.IsNullOrEmpty(EntryLname);
            if (ShowLastNameError)
                LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
            else
                LastNameError = string.Empty;
        }
        #endregion


        #region birthdate
        private DateTime entryBirthDate = DateTime.Now;
        public DateTime EntryBirthDate
        {
            get => this.entryBirthDate;
            set
            {
                if (value != this.entryBirthDate)
                {
                    this.entryBirthDate = value;
                    ValidateBirthDate();
                    OnPropertyChanged("EntryBirthDate");
                }
            }
        }

        private bool showBirthDateError;
        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate;
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;
        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int MIN_AGE = 18;
        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.EntryBirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);

            if (ShowBirthDateError)
                BirthDateError = ERROR_MESSAGES.BAD_DATE;
            else
                BirthDateError = string.Empty;
        }
        #endregion


        #region Radius
        private string entryRadius;
        public string EntryRadius
        {
            get => this.entryRadius;
            set
            {
                if (value != this.entryRadius)
                {
                    this.entryRadius = value;
                    ValidateRadius();
                    OnPropertyChanged("EntryRadius");
                }
            }
        }

        private bool showRadiusError;
        public bool ShowRadiusError
        {
            get => showRadiusError;
            set
            {
                showRadiusError = value;
                OnPropertyChanged("ShowRadiusError");
            }
        }


        private string radiusError;
        public string RadiusError
        {
            get => radiusError;
            set
            {
                radiusError = value;
                OnPropertyChanged("RadiusError");
            }
        }


        private void ValidateRadius()
        {
            this.ShowRadiusError = string.IsNullOrEmpty(entryRadius);
            if (!this.ShowRadiusError)
            {
                bool isOK = double.TryParse(entryRadius, out _);
                if (!isOK)
                {
                    this.ShowRadiusError = true;
                    this.RadiusError = ERROR_MESSAGES.BAD_RADIUS;
                }
            }
            else
                this.RadiusError = ERROR_MESSAGES.REQUIRED_FIELD;

        }

        #endregion


        #region is worker
        private bool isWorker;
        public bool IsWorker
        {
            get => this.current.User.IsWorker;
            set
            {
                if (value != this.isWorker)
                {
                    this.isWorker = value;

                    OnPropertyChanged("IsWorker");
                }

            }
        }
        #endregion


        #region Update
        private bool ValidateForm()
        {
            ValidateName();
            ValidateLastName();
            ValidateAp();
            ValidateBirthDate();
            ValidateCity();
            ValidateHN();
            ValidateRadius();
            ValidateStreet();
            ValidatePassword();

            //check if any validation failed
            if (ShowNameError || ShowLastNameError || ShowApError || ShowBirthDateError || ShowCityError || ShowHNError || ShowPassErorr || ShowStreetError)
                return false;
            else
            {
                if (isWorker && showRadiusError)
                    return false;
            }

            return true;
        }

        //public ICommand UpdateCommand { get; set; }
        public ICommand UpdateDetailsCommand => new Command(UpdateUserDetails);


        private async void UpdateUserDetails()
        {
            this.ShowNameError = false;
            this.ShowLastNameError = false;
            this.ShowApError = false;
            this.ShowBirthDateError = false;
            this.ShowCityError = false;
            this.ShowHNError = false;
            this.ShowRadiusError = false;
            this.ShowStreetError = false;
            this.showPassErorr = false;
            this.showRadiusError = false;

            if (ValidateForm())
            {
                User user = this.current.User;
                IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();

                bool isUpdated = false;
                //if (double.Parse(EntryRadius) != this.current.Worker.RadiusKm || this.current.Worker.WorkerServices != selectedServices)
                if(isWorker)
                {
                    //Worker w = this.current.Worker;
                    //w.UserPswd = EntryPass;
                    //w.FirstName = EntryFname;
                    //w.LastName = EntryLname;
                    //w.City = EntryCity;
                    //w.Birthday = EntryBirthDate;
                    //w.Street = EntryStreet;
                    //w.Apartment = EntryAp;
                    //w.HouseNumber = EntryHN;
                    //w.IsWorker = true;
                    //w.RadiusKm = double.Parse(EntryRadius);
                    //w.WorkerServices = new List<WorkerService>();
                    //foreach (Service s in workerServices)
                    //{
                    //    w.WorkerServices.Add(new WorkerService() { Service = s });
                    //}
                    //w = await IDAproxy.WorkerUpdate(w);
                    //if (w != null)
                    //    isUpdated = true;


                }
                else
                {
                    user.Id = this.current.User.Id;
                    user.UserPswd = EntryPass;
                    user.FirstName = EntryFname;
                    user.LastName = EntryLname;
                    user.City = City;
                    user.Birthday = EntryBirthDate;
                    user.Street = Street;
                    user.Apartment = EntryAp;
                    user.HouseNumber = EntryHN;

                    user = await IDAproxy.UserUpdate(user);
                    if (user != null)
                        isUpdated = true;

                }

                if (isUpdated)
                {
                    await App.Current.MainPage.DisplayAlert("", "Your details changed succsessfuly ", "Ok");
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("", "failed, please try again", "Ok");
                }


            }


        }


        #endregion


        #region services
        private Command getServices;

        public ICommand GetServices
        {
            get
            {
                if (getServices == null)
                {
                    getServices = new Command(ShowGetServices);
                }

                return getServices;
            }
        }

        private async void ShowGetServices()
        {

            if (Services == null)
            {
                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
                ((App)Application.Current).services = await IDAAPIProxy.GetServices();
                Services = new ObservableCollection<Service>();
                for (int i = 0; i < ((App)Application.Current).services.Count; i++)
                {
                    this.Services.Add(((App)Application.Current).services[i]);
                }
            }
            else
            {
                Services = null;
            }

        }

        //private Command selectServicesCommand;
        public ICommand SelectServicesCommand { get; set; }

        private void SelectServices()
        {
            if (workerServices == null)
                workerServices = new List<Service>();
            else
                workerServices.Clear();

            foreach (Service a in SelectedServices)
            {
                workerServices.Add(a);
            }

        }
        #endregion

    }


}

