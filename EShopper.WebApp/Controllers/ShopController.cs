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

        [Route("products/{category?}")] //products/telefon?page=2
        public IActionResult List(string category, int page=1)
        {
            const int pageSize= 3;
            return View(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems= _productService.GetCountbyCategory(category),
                    CurrentCategory=category,
                    CurrentPage=page,
                    ItemsPerPage=pageSize
                },
                Products=_productService.GetProductsByCategory(category,page,pageSize)
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
