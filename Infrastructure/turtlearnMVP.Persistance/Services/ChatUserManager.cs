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
    public class ChatUserManager : IChatUserService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ChatUserManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<ChatUser>();

        public IQueryable<ChatUserDTO> _QueryableChatUsers => _UnitOfWork.ChatUsers.GetAllQueryableRecords();

        public IDataResult<IList<ChatUserDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.ChatUsers.GetAllQueryableRecords();
            var ChatUserList = allQueryableRecords.ToList();
            return ChatUserList == null || ChatUserList.Count <= 0 ?
                new DataResult<List<ChatUserDTO>>(ResultStatus.Error, new List<ChatUserDTO>()) :
                new DataResult<List<ChatUserDTO>>(ResultStatus.Success, ChatUserList);
        }

        public async Task<IDataResult<ChatUser>> GetById(int id)
        {
            var chatUsers = await _UnitOfWork.ChatUsers.GetByIdAsync(id);
            return chatUsers == null || chatUsers.Id <= 0 ?
                new DataResult<ChatUser>(ResultStatus.Error, Messages.ResultIsNotFound, new ChatUser()) :
                new DataResult<ChatUser>(ResultStatus.Success, chatUsers);
        }

        public async Task<IResult> InsertAsync(ChatUser entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.ChatUsers.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(ChatUser entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.ChatUsers.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
