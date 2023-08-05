using EShopper.Entities;

namespace EShopper.WebApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
