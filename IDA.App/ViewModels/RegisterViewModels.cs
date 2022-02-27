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

namespace IDA.App.ViewModels
{

    class RegisterViewModel : ViewModelBase
    {

        public List<Service> SelectedServices { get; set; }

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
        }

        public RegisterViewModel()
        {
            App current = ((App)Application.Current);
            IsWorker = false;
            SelectedServices = new List<Service>();
            RegisterCommand = new Command(Register);
            SelectServicesCommand = new Command(SelectServices);
        }


        #region email
        private string entryEmail;
        public string EntryEmail
        {
            get => this.entryEmail;
            set
            {
                if (value != this.entryEmail)
                {
                    this.entryEmail = value;
                    ValidateEmail();
                    OnPropertyChanged("EntryEmail");
                }
            }
        }

        private bool showEmailError;
        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }


        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(entryEmail);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.entryEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region city
        private string entryCity;
        public string EntryCity
        {
            get => this.entryCity;
            set
            {
                if (value != this.entryCity)
                {
                    this.entryCity = value;
                    ValidateCity();
                    OnPropertyChanged("EntryCity");
                }
            }
        }

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
            this.ShowCityError = string.IsNullOrEmpty(entryCity);
            if (!this.ShowCityError)
            {
                this.ShowCityError = string.IsNullOrEmpty(entryCity);
            }
            else
                this.CityError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion


        #region Street
        private string entryStreet;
        public string EntryStreet
        {
            get => this.entryStreet;
            set
            {
                if (value != this.entryStreet)
                {
                    this.entryStreet = value;
                    ValidateStreet();
                    OnPropertyChanged("EntryStreet");
                }
            }
        }

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
            this.ShowStreetError = string.IsNullOrEmpty(entryStreet);
            if (!this.ShowStreetError)
            {
                this.ShowStreetError = string.IsNullOrEmpty(entryStreet);
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
            get => this.isWorker;
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


        #region register
        public ICommand RegisterCommand { get; set; }

        private async void Register()
        {
            IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();

            bool isEmailExist = await IDAproxy.EmailExistAsync(EntryEmail);
            if (!isEmailExist)
            {
                bool isRegister = false;
                if (IsWorker)
                {
                    Worker w = new Worker
                    {

                    };
                    w.Email = EntryEmail;
                    w.UserPswd = EntryPass;
                    w.FirstName = EntryFname;
                    w.LastName = EntryLname;
                    w.City = EntryCity;
                    w.Birthday = EntryBirthDate;
                    w.Street = EntryStreet;
                    w.Apartment = EntryAp;
                    w.HouseNumber = EntryHN;
                    w.IsWorker = true;
                    w.WorkerServices = new List<WorkerService>();
                    foreach (Service s in workerServices) // WORKERSERVICES COUNT = 0
                    {
                        w.WorkerServices.Add(new WorkerService() { Service = s, Worker = w });
                    }

                    w.RadiusKm = double.Parse(EntryRadius);
                    w.IsAvailble = false;


                    w = await IDAproxy.WorkerRegister(w);
                    if (w != null)
                    {
                        this.current.Worker = w;
                        this.current.User = w.IdNavigation;
                        isRegister = true;
                    }

                }
                else
                {
                    User user = new User();
                    user.Email = EntryEmail;
                    user.UserPswd = EntryPass;
                    user.FirstName = EntryFname;
                    user.LastName = EntryLname;
                    user.City = EntryCity;
                    user.Birthday = entryBirthDate;
                    user.Street = EntryStreet;
                    user.Apartment = EntryAp;
                    user.HouseNumber = EntryHN;


                    user = await IDAproxy.UserRegister(user);
                    if (user != null)
                    {
                        this.current.User = user;
                        //this.current.User = user.UserNameNavigation;
                        isRegister = true;
                    }

                }

                if (isRegister)
                {
                    TheMainTabbedPage theMainTabbedPage = (TheMainTabbedPage)Application.Current.MainPage;
                    //((TheMainTabbedPageViewModels)(theMainTabbedPage).BindingContext).LoginUser = ;
                    await App.Current.MainPage.DisplayAlert("", "You are logged in now!", "Ok");
                }

                else
                {
                    await App.Current.MainPage.DisplayAlert("", "Register failed, please try again", "Ok");
                }


            }

            else if (isEmailExist)
                await App.Current.MainPage.DisplayAlert(" ", "email already exsits please try another one", "ok", FlowDirection.RightToLeft);
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

