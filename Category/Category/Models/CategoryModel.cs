﻿using Category.Services;
using Category.ViewModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Category.Models
{
    public class CategoryModel
    {

        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }

        #endregion

        #region Servicios
        NavigationService navigationService;
        #endregion

        #region Ctor
        public CategoryModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        //tengo que crear aqui el tabgesture porque es el binding que gobierna al objeto

        #region Commands
        public ICommand SelectCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectCategory);
            }
        }
      async void SelectCategory()
        {

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Products = new ProductViewModel(Products);
            await navigationService.Navigate("ProductsView");


        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        private async void Edit()
        {
            var mainViewModel = MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this) ;

            await navigationService.Navigate("EditCategoryView");
        }
        #endregion


        #region Methods
        public override int GetHashCode()
        {
            //esto lo hacemos para que funcione el PUT
            return CategoryId;
        }
        #endregion
    }
}
