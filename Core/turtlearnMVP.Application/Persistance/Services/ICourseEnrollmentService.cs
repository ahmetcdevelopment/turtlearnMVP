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
    public interface ICourseEnrollmentService
    {
        IQueryable<CourseEnrollmentDTO> _QueryableCourseEnrollments { get; }
        Task<IResult> InsertAsync(CourseEnrollment entity);
        Task<IResult> UpdateOrDelete(CourseEnrollment entity);
        IDataResult<IList<CourseEnrollmentDTO>> FetchAllDtos();
        Task<IDataResult<CourseEnrollment>> GetById(int id);
        Task<IResult> BulkInsert(List<CourseEnrollment> _entityList);
    }
}
