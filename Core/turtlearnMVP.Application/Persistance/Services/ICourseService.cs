using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ICourseService
    {
        IQueryable<CourseDTO> _QueryableCourses { get; }
        Task<IResult> InsertAsync(Course entity);
        Task<IResult> UpdateOrDelete(Course entity);
        IDataResult<IList<CourseDTO>> FetchAllDtos(Expression<Func<CourseDTO, bool>> filter= null);
        Task<IDataResult<Course>> GetById(int id);
    }
}
