using EShopper.Business.Abstract;
using EShopper.Entities;
using EShopper.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace EShopper.WebApp.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        
        [Route("admin/products")]
        public IActionResult ProductList()
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetAll().ToList()
            });
        }

        public IActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View(new ProductModel());
        }
        [HttpPost]
        public IActionResult CreateProduct(ProductModel model, List<IFormFile> files, int[] categoryIds)
        {
            ModelState.Remove("ProductCategories");
            ModelState.Remove("SelectedCategories");
            ModelState.Remove("Images");
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        Image image = new Image();
                        image.ImageUrl = file.FileName;

                        entity.Images.Add(image);

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }
                    }
                }

                _productService.Create(entity,categoryIds);
                return RedirectToAction("ProductList");
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }

        [Route("admin/products/{id?}")]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategories(id.Value);

            if (entity == null) return NotFound();

            ProductModel model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Images = entity.Images,
                Price = entity.Price,
                SelectedCategories=entity.ProductCategories.Select(i=>i.Category).ToList()
            };

            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        [Route("admin/products/{id?}")]
        public IActionResult EditProduct(ProductModel model, List<IFormFile> files, int[] categoryIds)
        {
            var entity = _productService.GetById(model.Id);

            if (entity == null) return BadRequest();

            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;

            if (files.Count>0)
            {
                entity.Images.Clear();
                foreach (var file in files)
                {
                    Image image = new Image();
                    image.ImageUrl = file.FileName;

                    entity.Images.Add(image);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }
                }

            }

            _productService.Update(entity,categoryIds);

            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteProduct(int productId)
        {
            var model = _productService.GetById(productId);
            _productService.Delete(model);

            return RedirectToAction("ProductList");
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories=_categoryService.GetAll().ToList()
            });
        }
        public IActionResult CreateCategory()
        {
            return View(new CategoryModel());
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category()
                {
                    Name = model.Name
                };

                _categoryService.Create(cat);

                return RedirectToAction("CategoryList");
            }

            return View(model);
        }

        [Route("admin/categories/{id?}")]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category cat = _categoryService.GetByIdWithProducts((int)id);

            if (cat == null)
            {
                return BadRequest();
            }

            return View(new CategoryModel()
            {
                Id=cat.Id,
                Name=cat.Name,
                Products=cat.ProductCategories.Select(i=> i.Product).ToList()
            });
        }

        [HttpPost]
        [Route("admin/categories/{id?}")]
        public IActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category cat = _categoryService.GetById(model.Id);

                if (cat == null)
                {
                    return NotFound();
                }
                cat.Name = model.Name;

                _categoryService.Update(cat);
                return RedirectToAction("CategoryList");

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int? categoryId)
        {
            if (categoryId == null) return NotFound();

            Category cat = _categoryService.GetById((int)categoryId);
            _categoryService.Delete(cat);
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int productId)
        {
            _categoryService.DeleteFromCategory(categoryId, productId);

            return Redirect("/admin/categories/"+categoryId);
        }
    }
}
