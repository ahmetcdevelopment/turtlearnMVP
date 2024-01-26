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
    public interface IChatService
    {
        IQueryable<ChatDTO> _QueryableChats { get; }
        Task<IResult> InsertAsync(Chat entity);
        Task<IResult> UpdateOrDelete(Chat entity);
        IDataResult<IList<ChatDTO>> FetchAllDtos();
        Task<IDataResult<Chat>> GetById(int id);
    }
}
