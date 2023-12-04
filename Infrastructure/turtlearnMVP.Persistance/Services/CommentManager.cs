using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;

namespace turtlearnMVP.Persistance.Services
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<Comment>();

        public IQueryable<CommentDTO> _QueryableTenants => _UnitOfWork.Comments.GetAllQueryableRecords();

        public IDataResult<IList<CommentDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Comments.GetAllQueryableRecords();
            var CommentList = allQueryableRecords.ToList();
            return CommentList == null || CommentList.Count <= 0 ?
                new DataResult<List<CommentDTO>>(ResultStatus.Error, new List<CommentDTO>()) :
                new DataResult<List<CommentDTO>>(ResultStatus.Success, CommentList);
        }

        public async Task<IDataResult<Comment>> GetById(int id)
        {
            var comment = await _UnitOfWork.Comments.GetByIdAsync(id);
            return comment == null || comment.Id <= 0 ?
                new DataResult<Comment>(ResultStatus.Error, Messages.ResultIsNotFound, comment) :
                new DataResult<Comment>(ResultStatus.Success, comment);
        }

        public async Task<IResult> InsertAsync(Comment entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Comments.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Comment entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Comments.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
