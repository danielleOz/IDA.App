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
using IDA.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace IDA.App.ViewModels
{
    class UploadReviewViewModels:ViewModelBase
    {

        #region on submit
        public ICommand OnSubmitCommand => new Command(OnSubmitAsync);


        private async void OnSubmitAsync()
        {
            this.current.JobOffer.WorkerReviewDate = DateTime.Now;
            this.current.JobOffer.WorkerReviewDescriptipon = descriptoin;
            this.current.JobOffer.WorkerReviewRate = workerRating;

            IDAAPIProxy IDAproxy = IDAAPIProxy.CreateProxy();

            bool isOK = false;

            JobOffer j = this.current.JobOffer;
            j.User = null;
            j.ChosenWorker = null;
   
            j = await IDAproxy.JobOffer(j);

            if (j != null)
            {
                isOK = true;
            }

            if (isOK)
            {

                await App.Current.MainPage.DisplayAlert("", "your review has been submitted", "Ok");
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("", "failed please try again", "Ok");
            }

        }

        #endregion

        private int workerRating= 5;
        public int WorkerRating
        {
            get => this.workerRating;
            set
            {
                if (value != this.workerRating)
                {
                    this.workerRating = value;
                    OnPropertyChanged("WorkerRating");
                }
            }
        }
         
        public string WorkerName
        {
            get => this.current.JobOffer.ChosenWorker.FirstName;
        }

        public string UserName
        {
            get => this.current.JobOffer.User.FirstName;
        }

        public string ServiceType
        {
            get => this.current.JobOffer.Service.Name;
        }

        private string descriptoin;
        public string Descriptoin
        {
            get => this.descriptoin;
            set
            {
                if (value != this.descriptoin)
                {
                    this.descriptoin = value;
                    OnPropertyChanged("Descriptoin");
                }
            }
        }
    }
}
