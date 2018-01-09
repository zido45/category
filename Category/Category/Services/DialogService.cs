using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Category.Services
{
   public  class DialogService
    {
        public async Task ShowMessage(string title,string message)
        {
            await Application.Current.MainPage.DisplayAlert(title,message,"Accept");
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
           return  await Application.Current.MainPage.DisplayAlert(title, message, "Yes","No");
        }

    }
}
