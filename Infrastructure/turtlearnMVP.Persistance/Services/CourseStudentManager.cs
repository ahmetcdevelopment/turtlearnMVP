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
    public class CourseStudentManager : ICourseStudentService
    {
        public IQueryable<CourseStudentDTO> _QueryableCourseStudents => throw new NotImplementedException();

        public IDataResult<IList<CourseStudentDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CourseStudent>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(CourseStudent entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(CourseStudent entity)
        {
            throw new NotImplementedException();
        }
    }
}
