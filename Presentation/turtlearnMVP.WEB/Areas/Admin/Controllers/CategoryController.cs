using Microsoft.AspNetCore.Mvc;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategories()
        {
            // Sample data
            var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { Id = 1, Name = "AAAAA" },
            new CategoryViewModel { Id = 2, Name = "BBBBB" },
            new CategoryViewModel { Id = 3, Name = "CCCCC" }
            // Add more departments as needed
        };

            return Json(categories);
        }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
