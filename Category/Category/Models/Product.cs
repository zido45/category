using Category.Services;
using Category.ViewModels;
using GalaSoft.MvvmLight.Command;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Category.Models
{
    public class Product
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Properties
        [PrimaryKey]
        public int ProductId { get; set; }

        [ForeignKey(typeof(CategoryModel))]
        public int CategoryId { get; set; }
        [ManyToOne]
        public CategoryModel Category { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastPurchase { get; set; }

        public double Stock { get; set; }

        public string Remarks { get; set; }

        public byte[] ImageArray { get; set; }

        //public bool PendingToSave
        //{
        //    get;
        //    set;
        //}

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "noimage";
                }

                return string.Format(

                    "http://categoryapi.azurewebsites.net/{0}",
                    Image.Substring(1));
            }
        }
        #endregion

        #region Constructors
        public Product()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return ProductId;
        }
        #endregion

        #region Commands
       

        public ICommand EditCommand
    {
        get
        {
            return new RelayCommand(Edit);
        }
    }

        async void Edit()
        {
            MainViewModel.GetInstance().EditProduct =
                new EditProductViewModel(this);
            await navigationService.NavigateOnMaster("EditProductView");
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
            var response = await dialogService.ShowConfirm("Confirm", "Are you sure to delete this record?");
            if (!response)
            {
                return;
            }

            //lo borro en la otra pantalla ya que aqui no tengo nada que pintar luego
            await ProductViewModel.GetInstance().DeleteProduct(this);


        }
        #endregion
    }
}
