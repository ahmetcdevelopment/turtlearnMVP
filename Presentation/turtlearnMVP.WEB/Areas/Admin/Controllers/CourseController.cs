using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
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
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _mainService;
        private readonly ICategoryService _categoryService;
        public CourseController(ICourseService mainService, ICategoryService categoryService)
        {
            _mainService = mainService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            //var model = new CourseListViewModel();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new CourseEditViewModel();
            model.SelStatus = EnumHelper.GetEnumSelectList<CourseStatus>();
            model.SelCategories = GetCategoriesAsSelectList();
            if (id.HasValue && id.Value > 0)
            {
                var course = (await _mainService.GetById(id.Value)).Data ?? new Course(); // Assuming there is a GetCourseById method in _mainService
                model.Id = course.Id;
                model.TeacherId = course.TeacherId;
                model.CategoryId = course.CategoryId;
                model.StartDate = course.StartDate;
                model.EndDate = course.EndDate;
                model.Quota = course.Quota;
                model.Name = course.Name;
                model.PricePerHour = course.PricePerHour;
                model.TotalHour = course.TotalHour;
                model.TotalPrice = course.TotalPrice;
                model.Description = course.Description;
                model.Status = course.Status;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(CourseEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mainService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.TeacherId = model.TeacherId;
                result.Data.CategoryId = model.CategoryId;
                result.Data.StartDate = model.StartDate;
                result.Data.EndDate = model.EndDate;
                result.Data.Quota = model.Quota;
                result.Data.Name = model.Name;
                result.Data.PricePerHour = model.PricePerHour;
                result.Data.TotalHour = model.TotalHour;
                result.Data.TotalPrice = model.TotalPrice;
                result.Data.Description = model.Description;
                result.Data.Status = model.Status;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ? await _mainService.UpdateOrDelete(result.Data) : await _mainService.InsertAsync(result.Data);
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }

        public JsonResult GetAllToGrid()
        {
            var data = _mainService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _mainService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _mainService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }

        public SelectList GetAsSelectList()
        {
            var result = _mainService.FetchAllDtos();

            if (result.ResultStatus == ResultStatus.Success)
            {
                var categoryList = result.Data;
                var selectListItems = categoryList
                    .Select(category => new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    })
                    .ToList();
                var selectList = new SelectList(selectListItems, "Value", "Text");
                return selectList;
            }
            else
            {
                return null;
            }
        }

        public SelectList GetCategoriesAsSelectList()
        {
            var result = _categoryService.FetchAllDtos();

            if (result.ResultStatus == ResultStatus.Success)
            {
                var categoryList = result.Data;
                var selectListItems = categoryList
                    .Select(category => new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
                    })
                    .ToList();
                var selectList = new SelectList(selectListItems, "Value", "Text");
                return selectList;
            }
            else
            {
                return null;
            }
        }
    }
}
