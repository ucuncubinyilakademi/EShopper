using EShopper.Business.Abstract;
using EShopper.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.WebApp.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel()
            {
                SelectedCategory = RouteData.Values["category"]?.ToString(),
                Categories=_categoryService.GetAll().ToList()
            });
        }
    }
}
