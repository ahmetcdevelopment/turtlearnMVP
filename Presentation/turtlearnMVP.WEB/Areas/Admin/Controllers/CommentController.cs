using Microsoft.AspNetCore.Mvc;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
