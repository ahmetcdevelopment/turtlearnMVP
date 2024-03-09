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

public class HomeworkTransferManager : IHomeworkTransferService
{
    private readonly IUnitOfWork _UnitOfWork;

    public HomeworkTransferManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    private static string _tableNameTR = TableExtensions.GetTableTitle<HomeworkTransfer>();

    public IQueryable<HomeworkTransferDTO> _QueryableHomeworkTransfers => _UnitOfWork.HomeworkTransfers.GetAllQueryableRecords();

    public IDataResult<IList<HomeworkTransferDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.HomeworkTransfers.GetAllQueryableRecords();
        var HomeworkTransfers = allQueryableRecords.ToList();
        return HomeworkTransfers == null || HomeworkTransfers.Count <= 0 ?
            new DataResult<List<HomeworkTransferDTO>>(ResultStatus.Error, new List<HomeworkTransferDTO>()) :
            new DataResult<List<HomeworkTransferDTO>>(ResultStatus.Success, HomeworkTransfers);
    }

    public async Task<IDataResult<HomeworkTransfer>> GetById(int id)
    {
        var homeworkTransfer = await _UnitOfWork.HomeworkTransfers.GetByIdAsync(id);
        return homeworkTransfer == null || homeworkTransfer.Id <= 0 ?
            new DataResult<HomeworkTransfer>(ResultStatus.Error, Messages.ResultIsNotFound, homeworkTransfer) :
            new DataResult<HomeworkTransfer>(ResultStatus.Success, homeworkTransfer);
    }

    public async Task<IResult> InsertAsync(HomeworkTransfer entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.HomeworkTransfers.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(HomeworkTransfer entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.HomeworkTransfers.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IDataResult<int>> DetermineStatus(int homeworkId)
    {
        var homework = _UnitOfWork.Homeworks.GetById(homeworkId);
        if (homework == null || homework.Id <= 0)
            return new DataResult<int>(ResultStatus.Error, "Lütfen geçerli bir ödev seçiniz.", 0);

        var status = homework.EndDate < DateTime.Now ? (int)HomeworkTransferStatus.GecTeslim : (int)HomeworkTransferStatus.TeslimEdildi;

        return new DataResult<int>(ResultStatus.Success, status);
    }
}
