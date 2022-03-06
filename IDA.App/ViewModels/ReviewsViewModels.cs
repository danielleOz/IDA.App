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
    class ReviewsViewModels : ViewModelBase
    {
        private ObservableCollection<JobOffer> wR;
        public ObservableCollection<JobOffer> WR { get => WR; set { WR = value; OnPropertyChanged("WR"); } }

        public ObservableCollection<JobOffer> WReviews { get; }
        async void CreateWRCollection()
        {
            IDAAPIProxy proxy = IDAAPIProxy.CreateProxy();
            List<JobOffer> Wreviews = await proxy.GetWorkerReviews();
            foreach (JobOffer r in Wreviews)
            {
                this.WReviews.Add(r);
            }
        }

        #region is worker
        private bool isWorker;
        public bool IsWorker
        {
            get => this.current.User.IsWorker;
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
            get => this.current.User.IsWorker;
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
