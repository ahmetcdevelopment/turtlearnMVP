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
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;

namespace turtlearnMVP.Persistance.Services
{
    public class SessionManager : ISessionService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public SessionManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<Session>();

        public IQueryable<SessionDTO> _QueryableSessions => _UnitOfWork.Sessions.GetAllQueryableRecords();

        public IDataResult<IList<SessionDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Sessions.GetAllQueryableRecords();
            var SessionList = allQueryableRecords.ToList();
            return SessionList == null || SessionList.Count <= 0 ?
                new DataResult<List<SessionDTO>>(ResultStatus.Error, new List<SessionDTO>()) :
                new DataResult<List<SessionDTO>>(ResultStatus.Success, SessionList);
        }

        public async Task<IDataResult<Session>> GetById(int id)
        {
            var session = await _UnitOfWork.Sessions.GetByIdAsync(id);
            return session == null || session.Id <= 0 ?
                new DataResult<Session>(ResultStatus.Error, Messages.ResultIsNotFound, new Session()) :
                new DataResult<Session>(ResultStatus.Success, session); ;
        }

        public async Task<IResult> InsertAsync(Session entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Sessions.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Session entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Sessions.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IDataResult<SessionDetailApiDTO>> GetSessionApiDTO(int sessionId)
         => sessionId > 0 ? new DataResult<SessionDetailApiDTO>(ResultStatus.Success, await _UnitOfWork.Sessions.GetSessionDetails(sessionId))
            : new DataResult<SessionDetailApiDTO>(ResultStatus.Error, $"Aradığınız oturum mevcut değil", null);
    }
}
