using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Application.Persistance.Services;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseEnrollmentController : Controller
    {
        private readonly ICourseEnrollmentService _courseEnrollmentService;
        private readonly ICourseService _courseService;

        public CourseEnrollmentController(ICourseEnrollmentService courseEnrollmentService, ICourseService courseService)
        {
            _courseEnrollmentService = courseEnrollmentService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
