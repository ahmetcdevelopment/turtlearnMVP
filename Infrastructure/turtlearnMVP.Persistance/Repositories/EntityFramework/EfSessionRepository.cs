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
    public class EfSessionRepository : Repository<Session>, ISessionRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfSessionRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<SessionDTO> GetAllQueryableRecords()
        {
            var Query = from S in _Context.Sessions.DefaultIfEmpty()
                        join C in _Context.Courses.DefaultIfEmpty()
                        on S.CourseId equals C.Id
                        select new SessionDTO
                        {
                            Id = S.Id,
                            CourseId = S.CourseId,
                            CourseInfo = C.Name,
                            Queue = S.Queue,
                            SessionName = S.Name,
                            Description = S.Description,
                            StartDate = S.StartDate,
                            Link = S.Link,
                        };
            return Query;
        }
    }
}
