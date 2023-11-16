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
    public interface ICommentService
    {
        IQueryable<CommentDTO> _QueryableTenants { get; }
        Task<IResult> InsertAsync(Comment entity);
        Task<IResult> UpdateOrDelete(Comment entity);
        IDataResult<IList<CommentDTO>> FetchAllDtos();
        Task<IDataResult<Comment>> GetById(int id);
    }
}
