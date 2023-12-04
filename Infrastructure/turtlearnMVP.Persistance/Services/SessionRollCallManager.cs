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
    public class SessionRollCallManager : ISessionRollCallService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public SessionRollCallManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<SessionRollCall>();

        public IQueryable<SessionRollCallDTO> _QueryableCategories => _UnitOfWork.SessionRollCalls.GetAllQueryableRecords();

        public IDataResult<IList<SessionRollCallDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.SessionRollCalls.GetAllQueryableRecords();
            var SessionRollCallList = allQueryableRecords.ToList();
            return SessionRollCallList == null || SessionRollCallList.Count <= 0 ?
                new DataResult<List<SessionRollCallDTO>>(ResultStatus.Error, new List<SessionRollCallDTO>()) :
                new DataResult<List<SessionRollCallDTO>>(ResultStatus.Success, SessionRollCallList);
        }

        public async Task<IDataResult<SessionRollCall>> GetById(int id)
        {
            var sessionRollCall = await _UnitOfWork.SessionRollCalls.GetByIdAsync(id);
            return sessionRollCall == null || sessionRollCall.Id <= 0 ?
                new DataResult<SessionRollCall>(ResultStatus.Error, Messages.ResultIsNotFound, sessionRollCall) :
                new DataResult<SessionRollCall>(ResultStatus.Success, sessionRollCall);
        }

        public async Task<IResult> InsertAsync(SessionRollCall entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.SessionRollCalls.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(SessionRollCall entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.SessionRollCalls.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
