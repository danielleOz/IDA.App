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

namespace IDA.App.ViewModels
{
    class UploadReviewViewModels:ViewModelBase
    {

        #region on submit
        public ICommand OnSubmitCommand => new Command(OnSubmit);


        private void OnSubmit()
        {
            this.current.JobOffer.WorkerReviewDate = DateTime.Now;
            this.current.JobOffer.WorkerReviewDescriptipon = descriptoin;
            this.current.JobOffer.WorkerReviewRate = workerRating;

                
            
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
