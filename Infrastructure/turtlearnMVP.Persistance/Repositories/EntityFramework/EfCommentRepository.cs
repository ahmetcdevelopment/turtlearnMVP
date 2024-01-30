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
    public class EfCommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfCommentRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<CommentDTO> GetAllQueryableRecords()
        {
            var Query = from C in _Context.Comments
                        //join _course in _Context.Courses.DefaultIfEmpty()
                        //on C.CourseId equals _course.Id
                        join _user in _Context.Users.DefaultIfEmpty()
                        on C.StudentId equals _user.Id
                        where C.IsDeleted == true
                        select new CommentDTO
                        {
                            Id = C.Id,
                            //CourseId = _course.Id,
                            //CourseTitle = _course.Name,
                            ParentId = C.ParentId,
                            Text = C.Text,
                            StudentId = _user.Id,
                            StudentFirstName = _user.FirstName,
                            StudentLastName = _user.LastName,
                            Rating = C.Rating,
                        };
            return Query;
        }
    }
}
