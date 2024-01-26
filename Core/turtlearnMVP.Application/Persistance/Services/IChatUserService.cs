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
    public interface IChatUserService
    {
        IQueryable<ChatUserDTO> _QueryableChatUsers { get; }
        Task<IResult> InsertAsync(ChatUser entity);
        Task<IResult> UpdateOrDelete(ChatUser entity);
        IDataResult<IList<ChatUserDTO>> FetchAllDtos();
        Task<IDataResult<ChatUser>> GetById(int id);
    }
}
