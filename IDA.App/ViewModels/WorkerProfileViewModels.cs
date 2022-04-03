using IDA.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IDA.App.ViewModels
{
    class WorkerProfileViewModels:ViewModelBase
    {

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

    }
}
