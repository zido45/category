
namespace Category.Domain
{
    using System.Data.Entity;

    public class DataContext: DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<CategoryModel> CategoryModels { get; set; }

        public DbSet<Products> Products { get; set; }
    }
}
