using Microsoft.AspNetCore.Mvc;

namespace EShopper.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
