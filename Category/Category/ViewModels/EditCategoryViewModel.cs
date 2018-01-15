using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Category.Models;

namespace Category.ViewModels
{
    public class EditCategoryViewModel : INotifyPropertyChanged
    {
       


        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

        #region Atributos
        bool _isRunning;
        bool _isEnabled;
        private CategoryModel categoryModel;

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


        public EditCategoryViewModel(CategoryModel categoryModel)
        {
            //este constructor lo genero desde el modelo category y asi señalo que categoria quiero modificar.

            IsEnabled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            this.categoryModel = categoryModel;

            Description = categoryModel.Description;


        }
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands
        public ICommand SaveCategoryCommand
        {
            get
            {

                return new RelayCommand(Save);
            }

        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "You must enter a category description");
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

            categoryModel.Description = Description;

            var mainViewModel = MainViewModel.GetInstance();


            var response = await apiService.Put("http://categoryapi.azurewebsites.net", "/api", "/CategoryModels",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, categoryModel);


            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var categoriesViewModel = CategoriesViewModel.GetInstance();

            categoriesViewModel.UpdateCategory(categoryModel);
            await navigationService.BackOnMaster();

            IsRunning = false;
            IsEnabled = true;

        }
        #endregion
    }
}
