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
    public class CommentManager : ICommentService
    {
        public IQueryable<CommentDTO> _QueryableTenants => throw new NotImplementedException();

        public IDataResult<IList<CommentDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Comment>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
