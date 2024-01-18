using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;
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
    public class UserSettingManager : IUserSettingService
    {
        private readonly IUnitOfWork _UnitOfWork;
        //private readonly ISearch<UserSettingDTO> _Search;
        public UserSettingManager(IUnitOfWork unitOfWork/*, ISearch<CategoryDTO> search*/)
        {
            _UnitOfWork = unitOfWork;
            //_Search = search;
        }

        private static string _tableNameTR = TableExtensions.GetTableTitle<UserSetting>();

        public async Task<IDataResult<UserSetting>> GetById(int id)
        {
            var userSetting = await _UnitOfWork.UserSettings.GetByIdAsync(id);
            return userSetting == null || userSetting.Id <= 0 ?
                new DataResult<UserSetting>(ResultStatus.Error, Messages.ResultIsNotFound, new UserSetting()) :
                new DataResult<UserSetting>(ResultStatus.Success, userSetting);
        }

        public async Task<IResult> InsertAsync(UserSetting entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.UserSettings.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(UserSetting entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.UserSettings.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public Task<int> GenerateRandomVerificationCode()
        {
            Random random = new Random();
            return Task.FromResult(random.Next(100000, 999999));
        }
    }
}
