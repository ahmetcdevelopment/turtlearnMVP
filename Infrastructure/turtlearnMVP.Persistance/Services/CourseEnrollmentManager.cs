using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Services
{
    public class CourseEnrollmentManager : ICourseEnrollmentService
    {
        public IQueryable<CourseEnrollmentDTO> _QueryableCourseEnrollments => throw new NotImplementedException();

        public IDataResult<IList<CourseEnrollmentDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CourseEnrollment>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(CourseEnrollment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(CourseEnrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
