using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Category
{
    using Category.Models;
    using Category.Services;
    using Category.ViewModels;
    using Views;
	public partial class App : Application
	{

        #region servicios
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        NavigationService navigationService;
        #endregion
        public static NavigationPage Navigator { get; internal set; }
        public static MasterView Master { get; internal set; }

        public App ()
		{
			InitializeComponent();
            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();

            var token = dataService.First<TokenResponse>(false);
            if (token != null &&
            token.IsRemembered &&
            token.Expires > DateTime.Now)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = token;
                mainViewModel.Categories = new CategoriesViewModel();
                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }


         //   MainPage = new NavigationPage(new LoginView());
           // MainPage = new MasterView();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                  new NavigationPage(new LoginView()));
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {
            if (profile == null)
            {
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var apiService = new ApiService();
            var dialogService = new DialogService();

            var checkConnetion = await apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                await dialogService.ShowMessage("Error", checkConnetion.Message);
                return;
            }

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();
            var token = await apiService.LoginFacebook(
            urlAPI,
            "/api",
            "/Customers/LoginFacebook",
            profile);

            if (token == null)
            {
                await dialogService.ShowMessage(
                "Error",
                "Problem ocurred retrieving user information, try latter.");
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Categories = new CategoriesViewModel();
            Current.MainPage = new MasterView();
        }


    }
}
