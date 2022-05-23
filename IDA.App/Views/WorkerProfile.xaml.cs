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
    public partial class WorkerProfile : ContentPage
    {
        public WorkerProfile()
        {
            this.BindingContext = new WorkerProfileViewModels();
            InitializeComponent();
        }

        public WorkerProfile(WorkerProfileViewModels vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = this.BindingContext as WorkerProfileViewModels;
            await viewModel.GetList();
        }
    }
}