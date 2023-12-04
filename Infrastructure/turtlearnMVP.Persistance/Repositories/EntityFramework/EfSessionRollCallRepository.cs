using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfSessionRollCallRepository : Repository<SessionRollCall>, ISessionRollCallRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfSessionRollCallRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<SessionRollCallDTO> GetAllQueryableRecords()
        {
            var Query = from SCR in _Context.SessionRollCalls.DefaultIfEmpty()
                        join S in _Context.Sessions.DefaultIfEmpty()
                        on SCR.SessionId equals S.Id
                        join C in _Context.Courses.DefaultIfEmpty()
                        on SCR.CourseId equals C.Id
                        join U in _Context.Users.DefaultIfEmpty()
                        on SCR.UserId equals U.Id
                        select new SessionRollCallDTO
                        {
                            Id = SCR.Id,
                            CourseId = C.Id,
                            CourseName = C.Name,
                            SessionId = S.Id,
                            SessionTitle = S.Name,
                            StudentFirstName = U.FirstName,
                            StudentLastName = U.LastName,
                            TimeToJoin = SCR.TimeToJoin,
                        };
            return Query;
        }
    }
}
