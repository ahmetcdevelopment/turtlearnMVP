using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using turtlearnMVP.Domain.DTOs.SearchCritreiaDTOs;

namespace turtlearnMVP.Application.Persistance.SearchFilters
{
    public interface ICourseFilter<T>
    {
        Expression<Func<T, bool>> CreateFilter(CourseCriteria criteria);
    }
}
