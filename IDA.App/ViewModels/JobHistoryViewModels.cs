using IDA.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDA.App.ViewModels
{
    class JobHistoryViewModels :ViewModelBase
    {
        public JobHistoryViewModels()
        {

        }

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

        private JobOffer chosen;
        public JobOffer Chosen
        {
            get => chosen;
            set
            {
                chosen = value;
                OnPropertyChanged("Chosen");
            }
        }



        public JobHistoryViewModels(List<JobOffer> jobOffers)
        {
            List<JobOffer> filtered = jobOffers.Where(j => j.PublishDate != null).ToList();
            this.JobOffers = new ObservableCollection<JobOffer>(filtered);
        }


        #region go to upload review page
        public ICommand UploadCommand => new Command(upload);
        public void upload()
        {
            this.current.JobOffer = Chosen;
            UploadReviewViewModels vm = new UploadReviewViewModels();
            Page NewPage = new Views.UploadReview();
            NewPage.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(NewPage);

        }
        #endregion
    }
}
