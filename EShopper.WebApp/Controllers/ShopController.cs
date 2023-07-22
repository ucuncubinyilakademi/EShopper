using EShopper.Business.Abstract;
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
    }
}
