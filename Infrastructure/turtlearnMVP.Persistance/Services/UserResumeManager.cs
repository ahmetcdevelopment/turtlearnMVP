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

namespace turtlearnMVP.Persistance.Services;

public class UserResumeManager : IUserResumeService
{
    private readonly IUnitOfWork _UnitOfWork;

    public UserResumeManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    private static string _tableNameTR = TableExtensions.GetTableTitle<Category>();

    public IQueryable<UserResumeDTO> _QueryableResumes => _UnitOfWork.UserResumes.GetAllQueryableRecords();

    public IDataResult<IList<UserResumeDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.UserResumes.GetAllQueryableRecords();
        var userResumeList = allQueryableRecords.ToList();
        userResumeList.ForEach(userResume =>
        {
            userResume.ResumeTypeTitle = userResume.ResumeType > 0 ?
            EnumExtensions.GetEnumTitle<ResumeType>(userResume.ResumeType) : string.Empty;
        });
        return userResumeList == null || userResumeList.Count <= 0 ?
            new DataResult<List<UserResumeDTO>>(ResultStatus.Error, new List<UserResumeDTO>()) :
            new DataResult<List<UserResumeDTO>>(ResultStatus.Success, userResumeList);
    }

    public async Task<IDataResult<UserResume>> GetById(int id)
    {
        var userResume = await _UnitOfWork.UserResumes.GetByIdAsync(id);
        return userResume == null || userResume.Id <= 0 ?
            new DataResult<UserResume>(ResultStatus.Error, Messages.ResultIsNotFound, new UserResume()) :
            new DataResult<UserResume>(ResultStatus.Success, userResume);
    }

    public async Task<IResult> InsertAsync(UserResume entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.UserResumes.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(UserResume entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.UserResumes.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }
}
