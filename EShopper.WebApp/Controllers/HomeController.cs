using EShopper.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var model = _productService.GetAll().ToList();
            return View(model);
        }
    }
}
