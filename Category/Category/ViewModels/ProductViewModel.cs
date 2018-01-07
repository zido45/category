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

        #region Attributes
        private List<Product> products;
        ObservableCollection<Product> _products; //Esto es para que se refresque en tiempo de ejecucion la lista

        #endregion



        #region Properties
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
            this.products = products; //con este constructor me paso los productos que he seleccionado desde el categorymodel
            Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
        }


        #endregion




        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
