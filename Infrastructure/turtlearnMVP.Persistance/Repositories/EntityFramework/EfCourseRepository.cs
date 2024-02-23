using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework;

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
                    where C.IsDeleted == false
                    select new CourseDTO
                    {
                        Id = C.Id,
                        TeacherId = C.TeacherId,
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

    public async Task<CourseDetailApiDTO> GetCourseDetailsAsync(int courseId)
    {
        var courseQuery = GetAllQueryableRecords()
            .Where(course => course.Id == courseId)
            .Select(course => new CourseDetailApiDTO
            {
                Name = course.Name,
                PricePerHour = course.PricePerHour,
                CategoryName = course.CategoryName,
                Quota = course.Quota,
                Status = course.Status,
                TeacherFirstName = course.TeacherName,
                TeacherLastName = course.TeacherLastName,
                TotalHour = course.TotalHour,
                TotalPrice = course.TotalPrice,
                StartDate = course.StartDateStr,
                EndDate = course.EndDateStr,
                Description = course.Description,
            });

        var sessionQuery = await _Context.Sessions
            .Where(session => session.CourseId == courseId && !session.IsDeleted)
            .Select(session => new SessionDetailApiDTO
            {
                SessionName = session.Name,
                Description = session.Description,
                StartDate = session.StartDate.ToShortDateString(),
                Link = session.Link,
                Queue = session.Queue,
            })
            .ToListAsync();

        var course = await courseQuery.SingleOrDefaultAsync();

        if (course != null)
        {
            course.Sessions = sessionQuery;
        }

        return course ?? new CourseDetailApiDTO();
    }

}
