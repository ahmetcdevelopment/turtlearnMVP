using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ICourseStudentService
    {
        IQueryable<CourseStudentDTO> _QueryableCourseStudents { get; }
        Task<IResult> InsertAsync(CourseStudent entity);
        Task<IResult> UpdateOrDelete(CourseStudent entity);
        IDataResult<IList<CourseStudentDTO>> FetchAllDtos();
        Task<IDataResult<CourseStudent>> GetById(int id);
    }
}
