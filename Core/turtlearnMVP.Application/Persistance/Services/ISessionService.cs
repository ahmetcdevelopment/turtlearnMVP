using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ISessionService
    {
        IQueryable<SessionDTO> _QueryableSessions { get; }
        Task<IResult> InsertAsync(Session entity);
        Task<IResult> UpdateOrDelete(Session entity);
        IDataResult<IList<SessionDTO>> FetchAllDtos();
        Task<IDataResult<Session>> GetById(int id);
        Task<IDataResult<SessionDetailApiDTO>> GetSessionApiDTO(int sessionId);
    }
}
