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


        //#region go to upload review page
        //public ICommand UploadCommand => new Command(upload);
        //public void upload()
        //{
        //    UploadReviewViewModels vm = new UploadReviewViewModels();
        //    Page NewPage = new Views.UploadReview();
        //    NewPage.BindingContext = vm;
        //    App.Current.MainPage.Navigation.PushAsync(NewPage);

        //}
        //#endregion

    }
}
