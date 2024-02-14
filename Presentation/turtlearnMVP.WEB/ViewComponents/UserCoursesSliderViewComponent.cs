using Microsoft.AspNetCore.Mvc;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.WEB.ViewComponents
{
    public class UserCoursesSliderViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public UserCoursesSliderViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public IViewComponentResult Invoke(dynamic arguments)
        {
            var TeacherId = arguments.TeacherId;
            IList<CourseDTO> courses = _courseService.FetchAllDtos().Data.Where(x => x.TeacherId == TeacherId).ToList();

            return View(courses);
        }
    }
}
