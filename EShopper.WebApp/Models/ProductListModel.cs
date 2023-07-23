using EShopper.Entities;

namespace EShopper.WebApp.Models
{
    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class PageInfo
    {
        public int TotalItems { get; set;} //Toplam ürün sayısı
        public int ItemsPerPage { get; set; } //Her sayfa da kaç ürün olacak
        public int CurrentPage { get; set; } //Seçili sayfa 
        public string CurrentCategory { get; set; } //Seçili kategori

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
