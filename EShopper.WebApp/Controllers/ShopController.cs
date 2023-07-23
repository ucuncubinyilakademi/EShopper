using EShopper.Business.Abstract;
using EShopper.Entities;
using EShopper.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.WebApp.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult List()
        {           
            return View(new ProductListModel()
            {
                Products=_productService.GetAll().ToList()
            });
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel()
            {
                Product=product,
                Categories=product.ProductCategories.Select(i=>i.Category).ToList()
            });
        }
    }
}
