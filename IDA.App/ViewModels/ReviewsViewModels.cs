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
    class ReviewsViewModels:ViewModelBase
    {

        #region is worker
        private bool isWorker;
        public bool IsWorker
        {
            get => this.isWorker;
            set
            {
                if (this.current.User.IsWorker)
                {
                    this.isWorker = value;
                    OnPropertyChanged("IsWorker");
                }

            }
        }
        #endregion


        #region isnt worker
        private bool isntWorker;
        public bool IsntWorker
        {
            get => this.isntWorker;
            set
            {
                if (!this.current.User.IsWorker)
                {
                    this.isntWorker = value;
                    OnPropertyChanged("IsntWorker");
                }

            }
        }
        #endregion
    }
}
