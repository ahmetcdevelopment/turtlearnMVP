using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseEnrollmentController : Controller
    {
        private readonly ICourseEnrollmentService _mainService;
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userService;

        public CourseEnrollmentController(ICourseEnrollmentService courseEnrollmentService, ICourseService courseService, UserManager<User> userService)
        {
            _mainService = mainService;
            _courseService = courseService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public JsonResult GetAllToGrid()
        {
            var data = _courseEnrollmentService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        //Kurs başvurularını onaylamak için.
        [HttpGet]
        public async Task<IActionResult> Confirm(int id)
        {
            TurtLearn.Shared.Utilities.Results.Abstract.IResult result;
            if (id <= 0)
            {
                result = new Result(ResultStatus.Error, Messages.SomethingWrong);
                return Json(result);
            }
            var model = new CourseEnrollmentConfirmViewModel();
            var enrollmentResult = await _courseEnrollmentService.GetById(id);
            if (enrollmentResult.ResultStatus == ResultStatus.Success)
            {
                model.Id = enrollmentResult.Data.Id;
                var user = await _userService.Users.Where(x => x.Id == enrollmentResult.Data.Id).FirstOrDefaultAsync();
                model.FirstName = user != null ? user.FirstName : "";
                model.LastName = user != null ? user.LastName : "";
                var course = await _courseService.GetById(id);
                model.CourseName = course.ResultStatus == ResultStatus.Success ? course.Data.Name : "";
                model.Approved = enrollmentResult.Data.Approved;
                model.ApprovedStr = enrollmentResult.Data.Approved ? "Onaylandı" : "Onay Bekliyor";
                model.EnrollmentDate = enrollmentResult.Data.UpdateDate;
                return View(model);
            }
            else
            {
                result = new Result(ResultStatus.Error, Messages.SomethingWrong);
                return Json(result);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(CourseEnrollmentConfirmViewModel model)
        {
            if (model.Id < 0)
            {
                return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
            }
            var enrollmentResult = await _courseEnrollmentService.GetById(model.Id);
            if (enrollmentResult.ResultStatus == ResultStatus.Success)
            {
                enrollmentResult.Data.Approved = true;
                var insertResult = await _courseEnrollmentService.InsertAsync(enrollmentResult.Data);
                return Json(new { Result = insertResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }
        //başvur
        [HttpPost]
        public async Task<IActionResult> Apply(int? courseId)
        {
            if (courseId.HasValue)
            {
                var entity = new CourseEnrollment();
                entity.CourseId = courseId.Value;
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                entity.StudentId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                entity.UpdateDate = DateTime.Now;
                entity.IsDeleted = false;
                entity.UpdateUserId = userId != null && int.TryParse(userId, out int _uId) ? _uId : 0;
                entity.Approved = false;
                var saveResult = await _courseEnrollmentService.InsertAsync(entity);
                return Json(new { Result = saveResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
    }
}
