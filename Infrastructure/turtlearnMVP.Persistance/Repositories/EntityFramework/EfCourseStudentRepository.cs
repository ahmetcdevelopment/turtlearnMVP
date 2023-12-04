using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfCourseStudentRepository : Repository<CourseStudent>, ICourseStudentRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfCourseStudentRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<CourseStudentDTO> GetAllQueryableRecords()
        {
            var Query = from CS in _Context.CourseStudents.DefaultIfEmpty()
                        join C in _Context.Courses.DefaultIfEmpty()
                        on CS.CourseId equals C.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on CS.StudentId equals U.Id
                        select new CourseStudentDTO
                        {
                            Id = CS.Id,
                            StudentId = U.Id,
                            CourseId = CS.CourseId,
                            CourseTitle = C.Name,
                            StudentFirstName = U.FirstName,
                            StudentLastName = U.LastName,
                        };
            return Query;
        }
    }
}
