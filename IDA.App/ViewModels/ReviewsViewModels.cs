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
using System.Linq;

namespace IDA.App.ViewModels
{
    class ReviewsViewModels : ViewModelBase
    {
        public ReviewsViewModels()
        {

        }

        #region reviews
        private ObservableCollection<JobOffer> jobOffers;
        public ObservableCollection<JobOffer> JobOffers
        {
            get
            {
                return this.jobOffers;
            }
            set
            {
                this.jobOffers = value;
                OnPropertyChanged("JobOffers");
            }
        }


        #endregion

        public ReviewsViewModels(List<JobOffer> jobOffers)
        {
            List<JobOffer> filtered = jobOffers.Where(j => j.WorkerReviewDate != null).ToList();
            this.JobOffers = new ObservableCollection<JobOffer>(filtered);
        }


        #region is worker
        public bool IsWorker
        {
            get
            {
                if (this.current != null && this.current.User != null)
                    return this.current.User.IsWorker;
                return false;
            }
        }
        #endregion


        #region isnt worker
        public bool IsntWorker
        {
            get
            {
                if (this.current != null && this.current.User != null)
                    return !this.current.User.IsWorker;
                return false;
            }
        }
        #endregion



    }
}
