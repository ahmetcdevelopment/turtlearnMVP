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

public class OfferManager : IOfferService
{
    private readonly IUnitOfWork _UnitOfWork;

    public OfferManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    private static string _tableNameTR = TableExtensions.GetTableTitle<Offer>();
    public IQueryable<OfferDTO> _QueryableOffers => _UnitOfWork.Offers.GetAllQueryableRecords();

    public IDataResult<IList<OfferDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.Offers.GetAllQueryableRecords();
        var offerList = allQueryableRecords.ToList();
        offerList.ForEach(offer =>
        {
            offer.TypeTitle = offer.Type > 0 ? EnumExtensions.GetEnumTitle<OfferType>(offer.Type) : string.Empty;
        });
        return offerList == null || offerList.Count <= 0 ?
            new DataResult<List<OfferDTO>>(ResultStatus.Error, new List<OfferDTO>()) :
            new DataResult<List<OfferDTO>>(ResultStatus.Success, offerList);
    }

    public async Task<IDataResult<Offer>> GetById(int id)
    {
        var offer = await _UnitOfWork.Offers.GetByIdAsync(id);
        return offer == null || offer.Id <= 0 ?
            new DataResult<Offer>(ResultStatus.Error, Messages.ResultIsNotFound, new Offer()) :
            new DataResult<Offer>(ResultStatus.Success, offer);
    }

    public async Task<IResult> InsertAsync(Offer entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.Offers.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(Offer entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.Offers.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }
}
