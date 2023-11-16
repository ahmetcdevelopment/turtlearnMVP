using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Services
{
    public class SessionManager : ISessionService
    {
        public IQueryable<SessionDTO> _QueryableSessions => throw new NotImplementedException();

        public IDataResult<IList<SessionDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Session>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(Session entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(Session entity)
        {
            throw new NotImplementedException();
        }
    }
}
