using IDA.App.Models;
using IDA.App.Services;
using IDA.App.Views;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace IDA.App.ViewModels
{
    class WorkerDViewModels: ViewModelBase
    {
        #region name 
        private string name;
        public string Name
        {
            get => current.User.FirstName;
            set { }

        }
        #endregion

    }
}
