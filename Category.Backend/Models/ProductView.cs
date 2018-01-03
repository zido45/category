

namespace Category.Backend.Models
{
    using Category.Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    [NotMapped]
    public class ProductView :Products
    {
        [Display(Name ="Image")]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}