using Microsoft.AspNetCore.Mvc;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class HomeworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
