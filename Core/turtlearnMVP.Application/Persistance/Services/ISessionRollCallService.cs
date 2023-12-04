using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ISessionRollCallService
    {
        IQueryable<SessionRollCallDTO> _QueryableCategories { get; }
        Task<IResult> InsertAsync(SessionRollCall entity);
        Task<IResult> UpdateOrDelete(SessionRollCall entity);
        IDataResult<IList<SessionRollCallDTO>> FetchAllDtos();
        Task<IDataResult<SessionRollCall>> GetById(int id);
    }
}
