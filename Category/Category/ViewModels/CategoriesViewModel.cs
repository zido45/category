using Category.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Category.ViewModels
{
    public class CategoriesViewModel
    {
        #region Properties
        public ObservableCollection<CategoryModel> Categories { get; set; }
        #endregion
        #region Ctor
        public CategoriesViewModel()
        {
            LoadCategories();
        }


        #endregion


        #region Methods
        private void LoadCategories()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
