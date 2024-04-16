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
    public interface IUserSettingService
    {
        //IQueryable<UserSettingDTO> _QueryableCategories { get; }
        Task<IResult> InsertAsync(UserSetting entity);
        Task<IResult> UpdateOrDelete(UserSetting entity);
        //IDataResult<IList<UserSettingDTO>> FetchAllDtos();
        Task<IDataResult<UserSetting>> GetById(int id);
        Task<int> GenerateRandomVerificationCode();
        Task<IDataResult<UserSetting>> GetByUserIdAndKey(int userId, int typeId, int key);
    }
}
