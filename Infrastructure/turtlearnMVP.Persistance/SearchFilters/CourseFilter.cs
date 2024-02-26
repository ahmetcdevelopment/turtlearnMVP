using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Application.Persistance.SearchFilters;
using turtlearnMVP.Application.Persistance.Services;
//using turtlearnMVP.Application.SearchFilters;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.SearchCritreiaDTOs;

namespace turtlearnMVP.Persistance.SearchFilters
{

    public class CourseFilter : ICourseFilter<CourseDTO>
    {
        private readonly ISearchService<CourseDTO> _searchService;
        public CourseFilter(ISearchService<CourseDTO> searchService)
        {

            _searchService = searchService;

        }

        public Expression<Func<CourseDTO, bool>> CreateFilter(CourseCriteria criteria)
        {
            Expression<Func<CourseDTO, bool>> filter = x => true;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                var newFilter = _searchService.StringContains("Name", criteria.Name);
                filter = _searchService.AndConcat(filter, newFilter).ToExpression(null);
            }

            return filter;
        }
    }
}
