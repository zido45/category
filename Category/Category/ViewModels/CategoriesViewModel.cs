using Category.Models;
using Category.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Category.ViewModels
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        #region Atributos
        ObservableCollection<CategoryModel> _categories; //Esto es para que se refresque en tiempo de ejecucion la lista
        List<CategoryModel> categories;
        bool _isRefreshing;
        string _filter;
        #endregion

        #region Servicios
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        #endregion
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
        void SaveCategoriesOnDB()
        {
            dataService.DeleteAll<CategoryModel>();
            dataService.DeleteAll<Product>();

            foreach (var category in categories)
            {
                dataService.Insert(category);
                dataService.Save(category.Products);
            }
        }

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

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    Search();
                    PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Filter)));
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
            dataService = new DataService();
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
                categories = dataService.Get<CategoryModel>(true);
                if (categories.Count==0)
                {
                    await dialogService.ShowMessage("Error", connection.Message);
                    return;
                }

            }
            else
            {

                var mainViewModel = MainViewModel.GetInstance();
                var urlAPI = Application.Current.Resources["URLAPI"].ToString();

                var response = await apiService.GetList<CategoryModel>(urlAPI, "/api", "/CategoryModels", mainViewModel.Token.TokenType, mainViewModel.Token.AccessToken);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);

                }

                //si llego hasta aqui ya tengo mi lista de categorias


                categories = (List<CategoryModel>)response.Result;
                SaveCategoriesOnDB();

            }

           // Search();
            CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public void AddCategory(CategoryModel category)
        {
           // IsRefreshing = true;
            LoadCategories();
            //categories.Add(category);
          //  CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
          //  IsRefreshing = false;
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


        public async Task DeleteCategory(CategoryModel category)
        {
            IsRefreshing = true;
       

        

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }


            var mainViewModel = MainViewModel.GetInstance();
            var urlAPI = Application.Current.Resources["URLAPI"].ToString();


            var response = await apiService.Delete(urlAPI, "/api", "/CategoryModels",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken, category);


            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            categories.Remove(category);
            CategoriesList = new ObservableCollection<CategoryModel>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #region Commands
        public ICommand RefreshCommand {

            get
            {
                return new RelayCommand(LoadCategories);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }


        void Search()
        {
            IsRefreshing = true;

            if (string.IsNullOrEmpty(Filter))
            {
                CategoriesList = new ObservableCollection<CategoryModel>(
                categories.OrderBy(c => c.Description));
            }
            else
            {
                CategoriesList = new ObservableCollection<CategoryModel>(categories
                .Where(c => c.Description.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.Description));
            }

            IsRefreshing = false;
        }



        #endregion
    }
}
