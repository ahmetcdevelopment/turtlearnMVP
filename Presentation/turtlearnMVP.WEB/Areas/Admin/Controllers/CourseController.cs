using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TurtLearn.Shared.Searching;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.QueryDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _CourseService;
        private readonly ICategoryService _CategoryService;
        private readonly ISearch<CourseDTO> _Search;
        public CourseController(ICourseService courseService, ISearch<CourseDTO> search, ICategoryService categoryService)
        {
            _CourseService = courseService;
            _Search = search;
            _CategoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CourseListViewModel();
            return View(model);
        }
        public async Task<JsonResult> GetAllToGrid(CourseDTO courseDto)
        {
            var courses = _CourseService._QueryableCourses;
            courses = new CourseQueryDTO(_Search).GetFilteredData(courses, courseDto);
            return Json(await courses.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new CourseEditViewModel();
            model.SelStatus = EnumHelper.GetEnumSelectList<CourseStatus>();
            //model.SelCategories =await _CategoryService.getSelectList(new CategoryDTO());
            if (id.HasValue)
            {
                var Course = (await _CourseService.GetById(id.Value)).Data ?? new Course();
                model.Id = Course.Id;
                model.TeacherId = Course.TeacherId;
                model.CategoryId = Course.CategoryId;
                model.Name = Course.Name;
                model.PricePerHour = Course.PricePerHour;
                model.Description = Course.Description;
                model.Status = Course.Status;
                model.Quota = Course.Quota;
                model.TotalHour = Course.TotalHour;
                model.StartDate = Course.StartDate;
                model.EndDate = Course.EndDate;
                model.TotalPrice = Course.TotalPrice;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _CourseService.GetById(model.Id);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.CategoryId = model.CategoryId;
                result.Data.TeacherId = model.TeacherId;
                result.Data.Name = model.Name;
                result.Data.StartDate = model.StartDate;
                result.Data.EndDate = model.EndDate;
                result.Data.Quota = model.Quota;
                result.Data.PricePerHour = model.PricePerHour;
                result.Data.TotalHour = model.TotalHour;
                result.Data.TotalPrice = model.PricePerHour * model.TotalHour;
                result.Data.Status = model.Status;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                return Json(new { Result = result });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _CourseService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _CourseService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });

        }
    }
}
