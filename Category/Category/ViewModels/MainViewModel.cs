using Category.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Category.ViewModels
{
  
    public class MainViewModel
    {

        #region Propiedades
        public LoginViewModel Login { get; set; }
        public  TokenResponse Token { get; set; }
        public CategoriesViewModel Categories { get; set; }
        #endregion

        #region Ctor
        public MainViewModel()
        {
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
    }
}
