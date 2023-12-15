using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurtLearn.Shared.Searching;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.QueryDTOs;
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

            }
            return View(model);
        }
        [HttpPost]
        public Task<IActionResult> AddOrUpdate(CourseEditViewModel model)
        {
            throw new NotImplementedException();
        }
        public Task<JsonResult> Delete(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
