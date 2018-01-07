﻿using Category.Services;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace Category.ViewModels
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Eventos
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Atributos
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Services
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
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
        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }
        public string Email {
            get { return _email; }
            set {
                if (_email!=value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }
        #endregion

        #region Ctor
        public LoginViewModel()
        {
            IsEnabled = true;
            IsToggled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            Email = "zido45_dm@hotmail.com";
            Password = "Zido45_dm";
            navigationService = new NavigationService();
        }
        #endregion
        #region Commands
        public ICommand LoginCommand {
            get {
                return new RelayCommand(Login);
            }
        }

        async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Error", "You must enter an email");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "You must enter a Password");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error",connection.Message);
                return;
            }

            var response = await apiService.GetToken("http://categoryapi.azurewebsites.net", Email, Password);

            if (response==null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error","El servicio no esta disponible, por favor inténtalo mas tarde");
                Password = null;
                return;
            }


            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", response.ErrorDescription);
                Password = null;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = response;
            mainViewModel.Categories = new CategoriesViewModel();
           await navigationService.Navigate("CategoriesView");

            Email = null;
            Password = null;
            IsRunning = false;
            IsEnabled = true;


        }
        #endregion
    }
}
