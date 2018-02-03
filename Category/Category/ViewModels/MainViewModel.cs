using Category.Models;
using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Category.ViewModels
{
  
    public class MainViewModel
    {

        #region Propiedades
        public ObservableCollection<Menu> MyMenu
        {
            get;
            set;
        }

        public CategoryModel Category { get; set; }
        public LoginViewModel Login { get; set; }
        public  TokenResponse Token { get; set; }
        public ProductViewModel Products { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public NewCategoryViewModel NewCategory { get; set; }
        public NewProductViewModel NewProduct { get; set; }
        public EditCategoryViewModel EditCategory { get; set; }
        public NewCustomerViewModel NewCustomer { get; set; }
        public EditProductViewModel EditProduct { get; set; }


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
            LoadMenu();
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


        #region Methods
        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<Menu>();

            MyMenu.Add(new Menu
            {
                Icon = "ic_account_circle",
                PageName = "MyProfileView",
                Title = "Mi perfil",
            });


            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginView",
                Title = "Cerrar Sesion",
            });

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
            await navigationService.NavigateOnMaster("NewCategoryView");
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
            await navigationService.NavigateOnMaster("NewProductView");
            
       
        }
        #endregion
    }
}
