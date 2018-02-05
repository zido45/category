using Category.Services;
using Category.ViewModels;
using GalaSoft.MvvmLight.Command;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Category.Models
{
    public class CategoryModel
    {

        #region Properties
        [PrimaryKey]
        public int CategoryId { get; set; }
        public string Description { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }

        #endregion

        #region Servicios
        NavigationService navigationService;
        DialogService dialogService;
        #endregion

        #region Ctor
        public CategoryModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
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
            mainViewModel.Category = this; // me quedo la categoria para saber la relacion con el producto cuando cambie de pantalla
            mainViewModel.Products = new ProductViewModel(Products);
            await navigationService.NavigateOnMaster("ProductsView");


        }

        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            var response = await dialogService.ShowConfirm("Confirm","Are you sure to delete this record?");
            if (!response)
            {
                return;
            }

            //lo borro en la otra pantalla ya que aqui no tengo nada que pintar luego
           await  CategoriesViewModel.GetInstance().DeleteCategory(this);


        }

        private async void Edit()
        {
            var mainViewModel = MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this) ;

            await navigationService.NavigateOnMaster("EditCategoryView");
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
