using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Repositories
{
    public interface IChatRepository : IRepository<Chat>
    {
        IQueryable<ChatDTO> GetAllQueryableRecords();

    }
}
