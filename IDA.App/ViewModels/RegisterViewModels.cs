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
        private Workers worker;
        private Customers custmer;
        public List<object> SelectedServices { get; set; }

        private ObservableCollection<Service> services;
        public ObservableCollection<Service> Services { get => services; set { services = value; OnPropertyChanged("Services"); } }

        public static class ERROR_MESSAGES
        {
            public const string REQUIRED_FIELD = "This is a required field";
            public const string BAD_EMAIL = "invalid email";
            public const string SHORT_PASS = "password must contain at least 6 characters";
            public const string BAD_PHONE = "invalid phone";
            public const string BAD_DATE = "you must be above 18";
        }

        #region user name 
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

        private bool showUserNameError;
        public bool ShowUserNameError
        {
            get => showUserNameError;
            set
            {
                showUserNameError = value;
                OnPropertyChanged("ShowUserNameError");
            }
        }


        private string userNameError;
        public string UserNameError
        {
            get => userNameError;
            set
            {
                userNameError = value;
                OnPropertyChanged("UserNameError");
            }
        }

        private void ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(EntryUserName);
        }

        #endregion


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

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
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
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            this.ShowPassErorr= string.IsNullOrEmpty(EntryPass);
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
        }
        #endregion

        // need to vaidate
        #region adress
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


        #endregion


        #region birthdate
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
        }
        #endregion

        // need to add!!
        #region location
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


        public RegisterViewModel()
        {
            App current = ((App)Application.Current);
            IsWorker = false;
            SelectedServices = new List<object>();
            RegisterCommand = new Command(Register);
            SelectServicesCommand = new Command(SelectServices);


        }

     
        #region register
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

                Adult theAdult = new Adult()
                {
                    IdNavigation = user
                };

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();

                bool isEmailExist = await proxy.EmailExistAsync(theAdult.IdNavigation.Email);
                bool isUserNameExist = await proxy.UserNameExistAsync(theAdult.IdNavigation.UserName);

                if (!isEmailExist && !isUserNameExist)
                {
                    Adult newAdult = await proxy.AddAdultAsync(theAdult);
                    if (newAdult == null)
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "אישור", FlowDirection.RightToLeft);
                    }
                    else
                    {
                        if (this.imageFileResult != null)
                        {
                            ServerStatus = "מעלה תמונה...";

                            bool success = await proxy.UploadImage(new FileInfo()
                            {
                                Name = this.imageFileResult.FullPath
                            }, $"{newAdult.Id}.jpg");
                        }
                        ServerStatus = "שומר נתונים...";

                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                        await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                    }
                }
                else
                {
                    if (isEmailExist && isUserNameExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל ושם המשתמש שהקלדת כבר קיימים במערכת, בבקשה תבחר אימייל ושם משתמש חדשים ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else if (isEmailExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else
                        await App.Current.MainPage.DisplayAlert("שגיאה", "שם המשתמש שהקלדת כבר קיים במערכת, בבקשה תבחר שם משתמש חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
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


                Adult theAdult = new Adult()
                {
                    IdNavigation = user
                };

                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();

                bool isEmailExist = await proxy.EmailExistAsync(theAdult.IdNavigation.Email);
                bool isUserNameExist = await proxy.UserNameExistAsync(theAdult.IdNavigation.UserName);

                if (!isEmailExist && !isUserNameExist)
                {
                    Adult newAdult = await proxy.AddAdultAsync(theAdult);
                    if (newAdult == null)
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.DisplayAlert("שגיאה", "ההרשמה נכשלה", "אישור", FlowDirection.RightToLeft);
                    }
                    else
                    {
                        if (this.imageFileResult != null)
                        {
                            ServerStatus = "מעלה תמונה...";

                            bool success = await proxy.UploadImage(new FileInfo()
                            {
                                Name = this.imageFileResult.FullPath
                            }, $"{newAdult.Id}.jpg");
                        }
                        ServerStatus = "שומר נתונים...";

                        await App.Current.MainPage.Navigation.PopModalAsync();
                        await App.Current.MainPage.Navigation.PopToRootAsync();
                        await App.Current.MainPage.DisplayAlert("הרשמה", "ההרשמה בוצעה בהצלחה", "אישור", FlowDirection.RightToLeft);
                    }
                }
                else
                {
                    if (isEmailExist && isUserNameExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל ושם המשתמש שהקלדת כבר קיימים במערכת, בבקשה תבחר אימייל ושם משתמש חדשים ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else if (isEmailExist)
                        await App.Current.MainPage.DisplayAlert("שגיאה", "האימייל שהקלדת כבר קיים במערכת, בבקשה תבחר אימייל חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    else
                        await App.Current.MainPage.DisplayAlert("שגיאה", "שם המשתמש שהקלדת כבר קיים במערכת, בבקשה תבחר שם משתמש חדש ונסה שוב", "אישור", FlowDirection.RightToLeft);

                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
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

        #endregion



        #region services
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
        #endregion

    }

}
