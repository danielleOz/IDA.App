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

        #region worker name
        private string workerName;
        public string WorkerName
        {

            get
            {
                if(this.current.Worker == null)
                {
                    JobOffer currentJob = jobOffers.FirstOrDefault(o => o.UserId == this.current.User.Id);
                    if (currentJob != null)
                        return currentJob.ChosenWorker.FirstName;
                   
                    else
                        return " ";
                }

                else
                {
                    JobOffer currentJob = jobOffers.FirstOrDefault(o => o.ChosenWorker.Id == this.current.Worker.Id);
                    if (currentJob != null)
                        return currentJob.User.FirstName;

                    else
                        return " ";
                }

               


            }

        }


        //            if (Services == null)
        //            {
        //                IDAAPIProxy IDAAPIProxy = IDAAPIProxy.CreateProxy();
        //        ((App) Application.Current).services = await IDAAPIProxy.GetServices();
        //        Services = new ObservableCollection<Service>();
        //                for (int i = 0; i<((App) Application.Current).services.Count; i++)
        //                {
        //                    this.Services.Add(((App) Application.Current).services[i]);
        //                }
        //}
        //            else
        //{
        //    Services = null;
        //}


        #endregion

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

        public ReviewsViewModels(List<JobOffer> jobOffers)
        {
            List<JobOffer> filtered = jobOffers.Where(j => j.WorkerReviewDate != null).ToList();
            this.JobOffers = new ObservableCollection<JobOffer>(filtered);
        }

        #endregion
    }
}
