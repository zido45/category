using System;
using System.Collections.Generic;
using System.Text;


namespace Category.Models
{
    using Category.Services;
    using Category.ViewModels;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    public class Menu
    {

        #region Servicios
        NavigationService  navigationService;
        DataService dataService;
        #endregion
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Ctor
        public Menu()
        {
            navigationService = new NavigationService();
            dataService = new DataService();
        }
        #endregion
        #region Comandos
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        
        }

        private async void Navigate()
        {
            switch (PageName)
            {
                case "LoginView":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token.IsRemembered = false;
                    dataService.Update(mainViewModel.Token);
                    mainViewModel.Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;

                case "MyProfileView":
                   MainViewModel.GetInstance().MyProfile= new MyProfileViewModel();
                    await navigationService.NavigateOnMaster(PageName);
                    break;


            }
        }


        #endregion


    }
}
