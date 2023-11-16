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
    public class CourseManager : ICourseService
    {
        public IQueryable<CourseDTO> _QueryableCourses => throw new NotImplementedException();

        public IDataResult<IList<CourseDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Course>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
