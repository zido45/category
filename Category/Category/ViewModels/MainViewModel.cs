using Category.Models;
using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Category.ViewModels
{
  
    public class MainViewModel
    {

        #region Propiedades

        public CategoryModel Category { get; set; }
        public LoginViewModel Login { get; set; }
        public  TokenResponse Token { get; set; }
        public ProductViewModel Products { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public NewCategoryViewModel NewCategory { get; set; }
        public NewProductViewModel NewProduct { get; set; }
        public EditCategoryViewModel EditCategory { get; set; }
        #endregion


        #region Servicios
        NavigationService navigationService;
        #endregion
        #region Ctor
        public MainViewModel()
        {
            navigationService = new NavigationService();
            instance = this;
            Login = new LoginViewModel();
        }
        #endregion


        #region Singleton
        static MainViewModel instance;
        public static MainViewModel GetInstance()
        {

            if (instance==null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion


        #region Commands
        public ICommand NewCategoryCommand {

            get
            {
                return new RelayCommand(GoNewCategory);
            }

        }

        private async void GoNewCategory()
        {
            NewCategory = new NewCategoryViewModel();
            await navigationService.Navigate("NewCategoryView");
        }


         public ICommand NewProductCommand
        {

            get
            {
                return new RelayCommand(GoNewProduct);
            }

        }

        private async void GoNewProduct()
        {

            NewProduct = new NewProductViewModel();
            await navigationService.Navigate("NewProductView");
            
       
        }
        #endregion
    }
}
