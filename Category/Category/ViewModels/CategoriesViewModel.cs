using Category.Models;
using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Category.ViewModels
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        #region Atributos
        ObservableCollection<CategoryModel> _categories; //Esto es para que se refresque en tiempo de ejecucion la lista
        List<CategoryModel> categories;
        bool _isRefreshing;
        #endregion

        #region Servicios
        ApiService apiService;
        DialogService dialogService;

        #region Singleton
        static CategoriesViewModel instance;
        public static CategoriesViewModel GetInstance()
        {

            if (instance == null)
            {
                return new CategoriesViewModel();
            }

            return instance;
        }
        #endregion

        #region Metodos
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #endregion

        #region Properties
        public ObservableCollection<CategoryModel> CategoriesList
        {
            get { return _categories; }
            set {
                if (_categories!=value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoriesList)));
                }
            }

        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }

        }


        #endregion
        #region Ctor
        public CategoriesViewModel()
        {
            instance = this;
            dialogService = new DialogService();
            apiService = new ApiService();
            LoadCategories();
        }


        #endregion
        #region Methods
       async private void LoadCategories()
        {

            IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowMessage("Error",connection.Message);
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<CategoryModel>("http://categoryapi.azurewebsites.net", "/api", "/CategoryModels", mainViewModel.Token.TokenType,mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);

            }

            //si llego hasta aqui ya tengo mi lista de categorias


            categories =(List<CategoryModel>)response.Result;
            CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public void AddCategory(CategoryModel category)
        {
            IsRefreshing = true;
            categories.Add(category);
            CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public void UpdateCategory(CategoryModel category)
        {
            IsRefreshing = true;
            var oldCategory = categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            oldCategory = category;
            CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand {

            get
            {
                return new RelayCommand(LoadCategories);
            }
        }
        #endregion
    }
}
