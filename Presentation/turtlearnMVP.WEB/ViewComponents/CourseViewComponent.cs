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

        public IViewComponentResult Invoke(dynamic arguments)
        {
            IList<CourseDTO> courses = _courseService.FetchAllDtos().Data;
            string lastListingType = "grid";
            if (arguments.refresh)
            {
                lastListingType = HttpContext.Request.Cookies["LastListingType"];
            }

            string currentListingType = arguments.listingType ?? lastListingType;
            HttpContext.Response.Cookies.Append("LastListingType", currentListingType);
            ViewBag.Listing = currentListingType;

            return View(courses);
        }
    }
}
