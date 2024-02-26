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
    public async Task<SessionDetailApiDTO> GetSessionDetails(int sessionId)
    {
        var sessionQuery = GetAllQueryableRecords()
        .Where(session => session.Id == sessionId)
        .Select(session => new SessionDetailApiDTO
        {
            CourseInfo = session.CourseInfo,
            SessionName = session.SessionName,
            StartDate = session.StartDateStr,
            Link = session.Link,
            Queue = session.Queue,
            Description = session.Description,
        });

        var homeworkQuery = await _Context.Homeworks
            .Where(homework => homework.SessionId == sessionId && !homework.IsDeleted)
            .Select(homework => new HomeworkDetailApiDTO
            {
                Title = homework.Title,
                StartDate = homework.StartDate,
                EndDate = homework.EndDate,
                Description = homework.Description,
            })
            .ToListAsync();

        var commentQuery = await (from c in _Context.Comments.DefaultIfEmpty()
                                  join u in _Context.Users.DefaultIfEmpty()
                                  on c.StudentId equals u.Id
                                  where c.RecordId == sessionId && !c.IsDeleted
                                  select new CommentDetailApiDTO
                                  {
                                      StudentFirstName = u.FirstName,
                                      StudentLastName = u.LastName,
                                      Rating = c.Rating,
                                      Text = c.Text
                                  }).ToListAsync();

        var session = await sessionQuery.SingleOrDefaultAsync();

        if (session != null)
        {
            session.Homeworks = homeworkQuery;
            session.Comments = commentQuery;
        }

        return session ?? new SessionDetailApiDTO();
    }

}
