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
    public class EfCourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfCourseRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<CourseDTO> GetAllQueryableRecords()
        {
            var Query = from C in _Context.Courses
                        join _Category in _Context.Categories.DefaultIfEmpty()
                        on C.CategoryId equals _Category.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on C.TeacherId equals U.Id
                        where C.IsDeleted == true
                        select new CourseDTO
                        {
                            Id = C.Id,
                            CategoryName = _Category.Name,
                            TeacherName = U.FirstName,
                            TeacherLastName = U.LastName,
                            Name = C.Name,
                            StartDate = C.StartDate,
                            EndDate = C.EndDate,
                            Quota = C.Quota,
                            Status = C.Status,
                            PricePerHour = C.PricePerHour,
                            TotalHour = C.TotalHour,
                            TotalPrice = C.TotalPrice,
                            Description = C.Description,
                        };
            return Query;
        }
    }
}
