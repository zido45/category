using Category.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Category.Services
{
    public class NavigationService
    {

        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }


        public async Task NavigateOnMaster(string pagename)
        {
            App.Master.IsPresented = false;

            switch (pagename)
            {
                case "CategoriesView":
                    await App.Navigator.PushAsync(
                    new CategoriesView());
                    break;

                case "ProductsView":

                    await App.Navigator.PushAsync(
                       new ProductsView());
                    break;

                case "NewCategoryView":
                    await App.Navigator.PushAsync(
                                           new NewCategoryView());
                    break;

                case "NewProductView":

                    await App.Navigator.PushAsync(
                        new NewProductView());
                    break;

                case "EditCategoryView":

                    await App.Navigator.PushAsync(
                     new EditCategoryView());
                    break;
                case "EditProductView":

                    await App.Navigator.PushAsync(
                      new EditProductView());
                    break;
           

                default:
                    break;
            }
            
        }
        public async Task NavigateOnLogin(string pagename)
        {


            switch (pagename)
            {
               
                case "NewCustomerView":
                    await Application.Current.MainPage.Navigation.PushAsync(
                    new NewCustomerView());
                    break;
                case "LoginFacebookView":


                 
                        await Application.Current.MainPage.Navigation.PushAsync(
                      new LoginFacebookView());
               

           
                    break;

                default:
                    break;
            }

        }

        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
