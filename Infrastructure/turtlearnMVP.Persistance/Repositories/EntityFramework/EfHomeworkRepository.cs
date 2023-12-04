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
    public class EfHomeworkRepository : Repository<Homework>, IHomeworkRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfHomeworkRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<HomeworkDTO> GetAllQueryableRecords()
        {
            var Query = from H in _Context.Homeworks.DefaultIfEmpty()
                        join S in _Context.Sessions.DefaultIfEmpty()
                        on H.SessionId equals S.Id
                        select new HomeworkDTO
                        {
                            Id = H.Id,
                            SessionInfo = S.Name,
                            StartDate = H.StartDate,//ödevin başlangıç tarihi
                            EndDate = H.EndDate,//ödevin bitiş tarihi
                            Title = H.Title, // ödevin başlığı
                            Description = H.Description,
                        };
            return Query;
        }
    }
}
