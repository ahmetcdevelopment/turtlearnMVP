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
    public interface IMessageService
    {
        IQueryable<MessageDTO> _QueryableMessages { get; }
        Task<IResult> InsertAsync(Message entity);
        Task<IResult> UpdateOrDelete(Message entity);
        IDataResult<IList<MessageDTO>> FetchAllDtos();
        Task<IDataResult<Message>> GetById(int id);
    }
}
