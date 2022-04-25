using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

using IDA.App.Services;

namespace IDA.App.ViewModels
{
   public class ViewModelBase : INotifyPropertyChanged
    {

        protected IDAAPIProxy IDAproxy;
        public App current { get=>(App)Application.Current; }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModelBase()
        {
            IDAproxy = IDAAPIProxy.CreateProxy();
        }
        #endregion
    }
}
