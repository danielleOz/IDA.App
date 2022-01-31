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


    class RegisterViewModel : ViewModelBase
    {
        private Workers worker;
        private Customers custmer;
        public List<object> SelectedServices { get; set; }

        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Services { get => services; set { services = value; OnPropertyChanged("Services"); } }

        //Entrys 
        #region
        private string entryUserName;
        public string EntryUserName
        {
            get => this.entryUserName;
            set
            {
                if (value != this.entryUserName)
                {
                    this.entryUserName = value;
                    OnPropertyChanged("EntryUserName");
                }
            }
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

        private string entryFname;
        public string EntryFname
        {
            get => this.entryFname;
            set
            {
                if (value != this.entryFname)
                {
                    this.entryFname = value;
                    OnPropertyChanged("EntryFname");
                }
            }
        }


        private string entryLname;
        public string EntryLname
        {
            get => this.entryLname;
            set
            {
                if (value != this.entryLname)
                {
                    this.entryLname = value;
                    OnPropertyChanged("EntryLname");
                }
            }
        }


        private string entryAdress;
        public string EntryAdress
        {
            get => this.entryAdress;
            set
            {
                if (value != this.entryAdress)
                {
                    this.entryAdress = value;
                    OnPropertyChanged("EntryAdress");
                }
            }
        }


        private DateTime entryBirthDate;
        public DateTime EntryBirthDate
        {
            get => this.entryBirthDate;
            set
            {
                if (value != this.entryBirthDate)
                {
                    this.entryBirthDate = value;
                    OnPropertyChanged("EntryBirthDate");
                }
            }
        }

        private double entryLocation;
        public double EntryLocation
        {
            get => this.entryLocation;
            set
            {
                if (value != this.entryLocation)
                {
                    this.entryLocation = value;
                    OnPropertyChanged("EntryLocation");
                }
            }
        }

        #endregion


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


        public RegisterViewModel()
        {
            App current = ((App)Application.Current);
            IsWorker = false;
            SelectedServices = new List<object>();
            RegisterCommand = new Command(Register);
            SelectServicesCommand = new Command(SelectServices);


        }

        public ICommand RegisterCommand { get; set; }
        private async void Register()
        {
            bool isRegister = false;
            //add validation - (לעשות פעולה לכל אחד (איפה

            //if ((EntryUserName == "") || (EntryEmail == "") || (EntryPass == ""))
            // צריך לשים את כל השדות ככה? או שיש דרך אחרת? מבחינת הרשמה כעובד איך להבדיל בפעולה הזאת יש עוד שדות שקר עובדים ממלאים ומשתמשים לא
            //{
            //    await App.Current.MainPage.DisplayAlert("IDA", "Please fill all the fields", "Ok");
            //    return;
            //}
            User user = new User();
            user.UserName = EntryUserName;
            user.Email = EntryEmail;
            user.UserPswd = EntryPass;
            user.FirstName = EntryFname;
            user.LastName = EntryLname;
            user.Adress = EntryAdress;
            user.Birthday = entryBirthDate;
            IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();

            if (isWorker)
            {
                worker.Location = entryLocation;
                worker.UserNameNavigation = user;
                //add worker details

                worker = await IDAproxy.WorkerRegister(worker);
                if (worker != null)
                {
                    this.current.Worker = worker;
                    this.current.User = worker.UserNameNavigation;
                    isRegister = true;
                }

            }

            else
            {
                custmer = new Customers() { UserNameNavigation = user };

                custmer = await IDAproxy.CustomerRegister(custmer);
                if (custmer != null)
                {
                    this.current.Customer = custmer;
                    this.current.User = custmer.UserNameNavigation;
                    isRegister = true;
                }

            }

            if (isRegister)
            {
                TheMainTabbedPage theMainTabbedPage = (TheMainTabbedPage)Application.Current.MainPage;
                ((TheMainTabbedPageViewModels)(theMainTabbedPage).BindingContext).LoginUser = user;
                await App.Current.MainPage.DisplayAlert("IDA", "You are logged in now!", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("IDA", "Register failed, please try enter another fields", "Ok");
            }
            EntryEmail = "";
            EntryUserName = "";
            EntryPass = "";
        }

        private Command getServices;

        public ICommand GetServices
        {
            get
            {
                if (getServices == null)
                {
                    getServices = new Command(PerformGetServices);
                }

                return getServices;
            }
        }

        private async void PerformGetServices()
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

        private Command selectServicesCommand;
        public ICommand SelectServicesCommand { get; set; }

        private void SelectServices()
        {
            if (worker == null)
                worker = new Workers();

            worker.WorkerServices.Clear();
            worker.Services = string.Empty;
            foreach (object a in SelectedServices)
            {
                Service service = (Service)a;
                WorkerService s = new WorkerService() { SidNavigation = service };
                worker.WorkerServices.Add(s);
                if (worker.Services != string.Empty)
                    worker.Services += "," + service.Name;
                else
                    worker.Services += service.Name;

            }

        }
    }
}
