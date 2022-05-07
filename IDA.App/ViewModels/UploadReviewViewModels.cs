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
    class UploadReviewViewModels
    {

        #region on submit
        public ICommand OnSubmitCommand => new Command(OnSubmit);


        private void OnSubmit()
        {
            
            
        }

        #endregion

    }
}
