using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public CourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public IViewComponentResult Invoke()
        {
            IList<CourseDTO> courses = _courseService.FetchAllDtos().Data;

            return View(courses);
        }
    }
}
