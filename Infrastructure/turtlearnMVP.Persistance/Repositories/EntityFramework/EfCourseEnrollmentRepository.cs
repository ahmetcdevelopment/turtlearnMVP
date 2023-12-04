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
    public class EfCourseEnrollmentRepository : Repository<CourseEnrollment>, ICourseEnrollmentRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfCourseEnrollmentRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<CourseEnrollmentDTO> GetAllQueryableRecords()
        {
            var Query = from CE in _Context.CourseEnrollments
                        join C in _Context.Courses.DefaultIfEmpty()
                        on CE.CourseId equals C.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on CE.StudentId equals U.Id
                        where CE.IsDeleted == true
                        select new CourseEnrollmentDTO
                        {
                            Id = CE.Id,
                            StudentId = U.Id,
                            CourseTitle = C.Name,
                            StudentFirstName = U.FirstName,
                            StudentLastName = U.LastName,
                            Approved = CE.Approved,
                        };
            return Query;
        }
    }
}
