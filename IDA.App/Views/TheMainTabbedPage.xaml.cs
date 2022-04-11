using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
using Xamarin.Forms.Xaml;
using IDA.App.ViewModels;

namespace IDA.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheMainTabbedPage : Xamarin.Forms.TabbedPage
    {
        public Register register;
        public LogIn logIn;
        public JobOfferPage home ;
        public Profile profile;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModels();
            InitializeComponent();
            On<Windows>().SetHeaderIconsEnabled(true);
            On<Windows>().SetHeaderIconsSize(new Size(50, 50));

            home = new JobOfferPage();
            home.Title = "job offer";
            //home.IconImageSource = "home.png.png";
            //this.Children.Add(home);
            

            logIn = new LogIn();
            logIn.Title = "login";
            //logIn.IconImageSource = "loginnn.png.png";
            this.Children.Add(logIn);


            register = new Register();
            register.Title = "register";
            //register.IconImageSource = "signup.png";
            this.Children.Add(register);


            //profile = new Profile();
            //profile.Title = "profile";
            //profile.IconImageSource = "profile.png";
            //this.Children.Add(profile);

        }

        public void AddTab(Xamarin.Forms.Page p)
        {


            if (!this.Children.Contains(p))
                this.Children.Add(p);
        }

        public void AddTabLoggedIn(Xamarin.Forms.Page p)
        {


            if (!this.Children.Contains(p) )
                this.Children.Add(p);
        }

        public void RemoveTab(Xamarin.Forms.Page p)
        {
            if (this.Children.Contains(p))
                this.Children.Remove(p);
        }

        public void CurrentTab(Xamarin.Forms.Page p)
        {
            this.CurrentPage = p;
        }
    }


}