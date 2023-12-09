using Microsoft.AspNetCore.Mvc;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
