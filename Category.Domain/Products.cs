

namespace Category.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} character length.")]
        [Index("Product_DescriptionIndex", IsUnique = true)]
        public string Description { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
       [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        public decimal Price { get; set; }
        [Display(Name ="Is Active?")]
        public bool IsActive { get; set; }
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public double Stock { get; set; }
        [Display(Name = "Last Purchase")]
        [DataType(DataType.Date)]
        public DateTime LastPurchase { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual CategoryModel Category { get; set; }

        public string Image { get; set; }
    }
}
