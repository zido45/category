using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Category.Models;
using Xamarin.Forms;

namespace Category.ViewModels
{
    public class NewCategoryViewModel : INotifyPropertyChanged
    {

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Atributos
        bool _isRunning;
        bool _isEnabled;
       
        #endregion

        #region Methods
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Propiedades
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public string Description { get; set; }
        #endregion

        #region Ctor
        public NewCategoryViewModel()
        {
            IsEnabled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
        }


        #endregion

        #region Commands
        public ICommand SaveCategoryCommand {
            get
            {

                return new RelayCommand(Save);
            }

        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error","You must enter a category description");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var category = new CategoryModel
            {
                Description = Description,
            };

            var mainViewModel = MainViewModel.GetInstance();

            var urlAPI = Application.Current.Resources["URLAPI"].ToString();

            var response = await apiService.Post(urlAPI, "/api", "/CategoryModels",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,category);


            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            category = (CategoryModel)response.Result;
            var categoriesViewModel = CategoriesViewModel.GetInstance();

            categoriesViewModel.AddCategory(category);
            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion

    }
}
