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
    public partial class JobHistory : ContentPage
    {
        public JobHistory()
        {
            this.BindingContext = new JobHistoryViewModels();
            InitializeComponent();
        }
    }
}