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

        private void Navigate()
        {
            switch (PageName)
            {
                case "LoginView":
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    navigationService.SetMainPage("LoginView");
                    break;
           
            }
        }


        #endregion


    }
}
