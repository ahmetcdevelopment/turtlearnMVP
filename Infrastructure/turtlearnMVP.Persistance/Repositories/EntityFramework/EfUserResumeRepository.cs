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

namespace turtlearnMVP.Persistance.Repositories.EntityFramework;

public class EfUserResumeRepository : Repository<UserResume>, IUserResumeRepository
{
    private readonly turtlearnMVPContext _Context;
    public EfUserResumeRepository(DbContext context) : base(context)
    => _Context = context as turtlearnMVPContext;

    public IQueryable<UserResumeDTO> GetAllQueryableRecords()
    {
        var Query = from UR in _Context.UserResumes.DefaultIfEmpty()
                    join U in _Context.Users.DefaultIfEmpty()
                    on UR.UserId equals U.Id
                    select new UserResumeDTO
                    {
                        Id = UR.Id,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        ResumeType = UR.ResumeType,
                        Title = UR.Title,
                        Link = UR.Link,
                        StartDate = UR.StartDate,
                        EndDate = UR.EndDate,
                    };
        return Query;
    }
}
