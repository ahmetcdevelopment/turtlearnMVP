using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseEnrollmentController : Controller
    {
        private readonly ICourseEnrollmentService _mainService;
        private readonly ICourseService _courseService;

        public CourseEnrollmentController(ICourseEnrollmentService mainService, ICourseService courseService)
        {
            _mainService = mainService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new CourseEnrollmentEditViewModel();
            model.SelCourse = EnumHelper.GetEnumSelectList<SinifDuzeyi>();
            model.SelStudent = EnumHelper.GetEnumSelectList<SinifDuzeyi>();
            if (id.HasValue && id.Value > 0)
            {
                var main = (await _mainService.GetById(id.Value)).Data ?? new CourseEnrollment();
                model.Id = main.Id;
                model.CourseId = main.CourseId;
                model.Approved = main.Approved;
            }
            return View(model);
        }
    }
}
