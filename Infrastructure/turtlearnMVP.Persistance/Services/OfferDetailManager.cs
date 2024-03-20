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

public class OfferDetailManager : IOfferDetailService
{
    private readonly IUnitOfWork _UnitOfWork;

    public OfferDetailManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }

    public IQueryable<OfferDetailDTO> _QueryableOfferDetails => _UnitOfWork.OffersDetail.GetAllQueryableRecords();
    private static string _tableNameTR = TableExtensions.GetTableTitle<OfferDetail>();
    public IDataResult<IList<OfferDetailDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.OffersDetail.GetAllQueryableRecords();
        var offerDetailList = allQueryableRecords.ToList();
        return offerDetailList == null || offerDetailList.Count <= 0 ?
            new DataResult<List<OfferDetailDTO>>(ResultStatus.Error, new List<OfferDetailDTO>()) :
            new DataResult<List<OfferDetailDTO>>(ResultStatus.Success, offerDetailList);
    }

    public async Task<IDataResult<OfferDetail>> GetById(int id)
    {
        var offerDetail = await _UnitOfWork.OffersDetail.GetByIdAsync(id);
        return offerDetail == null || offerDetail.Id <= 0 ?
            new DataResult<OfferDetail>(ResultStatus.Error, Messages.ResultIsNotFound, new OfferDetail()) :
            new DataResult<OfferDetail>(ResultStatus.Success, offerDetail);
    }

    public async Task<IResult> InsertAsync(OfferDetail entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.OffersDetail.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(OfferDetail entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.OffersDetail.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }
}
