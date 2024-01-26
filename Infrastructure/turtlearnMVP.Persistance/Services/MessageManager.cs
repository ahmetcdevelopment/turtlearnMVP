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

namespace turtlearnMVP.Persistance.Services
{
    public class MessageManager : IMessageService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public MessageManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<ChatUser>();

        public IQueryable<MessageDTO> _QueryableMessages => _UnitOfWork.Messages.GetAllQueryableRecords();

        public IDataResult<IList<MessageDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Messages.GetAllQueryableRecords();
            var Messages = allQueryableRecords.ToList();
            return Messages == null || Messages.Count <= 0 ?
                new DataResult<List<MessageDTO>>(ResultStatus.Error, new List<MessageDTO>()) :
                new DataResult<List<MessageDTO>>(ResultStatus.Success, Messages);
        }

        public async Task<IDataResult<Message>> GetById(int id)
        {
            var message = await _UnitOfWork.Messages.GetByIdAsync(id);
            return message == null || message.Id <= 0 ?
                new DataResult<Message>(ResultStatus.Error, Messages.ResultIsNotFound, new Message()) :
                new DataResult<Message>(ResultStatus.Success, message);
        }

        public async Task<IResult> InsertAsync(Message entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Messages.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public  async Task<IResult> UpdateOrDelete(Message entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Messages.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
