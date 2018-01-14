using System;


namespace Category.API.Models
{
    public class ProductResponse
    {

      
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
       
        public string Remarks { get; set; }

        public double Stock { get; set; }
        public string Description { get; set; }

        public DateTime LastPurchase { get; set; }    
        public string Image { get; set; }
    }
}