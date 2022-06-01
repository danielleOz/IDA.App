using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

using IDA.App.Services;
using System.Runtime.CompilerServices;

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
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.NotifyPropertyChanged(propertyName);

            return true;
        }
        public ViewModelBase()
        {
            IDAproxy = IDAAPIProxy.CreateProxy();
        }
        #endregion
    }
}
