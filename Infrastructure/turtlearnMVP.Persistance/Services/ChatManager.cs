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
    public class ChatManager : IChatService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ChatManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<Chat>();

        public IQueryable<ChatDTO> _QueryableChats => _UnitOfWork.Chats.GetAllQueryableRecords();

        public IDataResult<IList<ChatDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Chats.GetAllQueryableRecords();
            var ChatList = allQueryableRecords.ToList();
            ChatList.ForEach(chat =>
            {
                chat.PrivacyTitle = chat.Privacy > 0 ?
                EnumExtensions.GetEnumTitle<ChatPrivacy>(chat.Privacy) : string.Empty; ;
            });
            return ChatList == null || ChatList.Count <= 0 ?
                new DataResult<List<ChatDTO>>(ResultStatus.Error, new List<ChatDTO>()) :
                new DataResult<List<ChatDTO>>(ResultStatus.Success, ChatList);
        }

        public async Task<IDataResult<Chat>> GetById(int id)
        {
            var chat = await _UnitOfWork.Chats.GetByIdAsync(id);
            return chat == null || chat.Id <= 0 ?
                new DataResult<Chat>(ResultStatus.Error, Messages.ResultIsNotFound, new Chat()) :
                new DataResult<Chat>(ResultStatus.Success, chat);
        }

        public async Task<IResult> InsertAsync(Chat entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Chats.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Chat entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Chats.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
