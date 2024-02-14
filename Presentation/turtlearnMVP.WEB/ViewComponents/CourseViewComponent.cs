using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Search;

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
            var search = new Search.Course();
            var criteria = new Search.Course.Criteria
            {
                Name = "matematik"
            };
            Expression<Func<CourseDTO, bool>> filter = search.CreateFilter(criteria);
            IList<CourseDTO> courses = _courseService.FetchAllDtos(filter).Data;
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
