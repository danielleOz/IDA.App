using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using IDA.App.ViewModels;
namespace IDA.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.BindingContext != null)
            {
                ProfileViewModels vm = new ProfileViewModels();
                this.BindingContext = vm;
            }
            base.OnAppearing();
        }
    }
}