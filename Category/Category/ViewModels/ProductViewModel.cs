using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Category.Models;

namespace Category.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
        #region Attributes
        private List<Product> products;
        bool _isRefreshing;
        ObservableCollection<Product> _products; //Esto es para que se refresque en tiempo de ejecucion la lista

        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Products)));
                }
            }

        }
        #endregion


        #region Ctor
        public ProductViewModel(List<Product> products)
        {
            instance = this;
            this.products = products; //con este constructor me paso los productos que he seleccionado desde el categorymodel
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }


        #endregion

        #region Singleton
        static ProductViewModel instance;

        public static ProductViewModel GetInstance()
        {
            return instance;
        }
        #endregion


        

        #region metodos

        public void Add(Product product)
        {
            IsRefreshing = true;
            products.Add(product);
            Products = new ObservableCollection<Product>(
                products.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion
    }
}
