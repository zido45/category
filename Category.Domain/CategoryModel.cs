

namespace Category.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="The field {0} is required")]
        [MaxLength(50,ErrorMessage ="The field {0} only can contain {1} character length.")]
        [Index("DescriptionIndex", IsUnique = true)]

        public string Description  { get; set; }

        [JsonIgnore]
        public virtual ICollection<Products> Products { get; set; }
    }
}
