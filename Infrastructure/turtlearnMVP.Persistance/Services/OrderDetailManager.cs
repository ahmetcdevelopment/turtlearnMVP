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

public class OrderDetailManager : IOrderDetailService
{
    private readonly IUnitOfWork _UnitOfWork;

    public OrderDetailManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    private static string _tableNameTR = TableExtensions.GetTableTitle<OrderDetail>();

    public IQueryable<OrderDetailDTO> _QueryableOrderDetails => _UnitOfWork.OrderDetails.GetAllQueryableRecords();

    public IDataResult<IList<OrderDetailDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.OrderDetails.GetAllQueryableRecords();
        var orderDetailList = allQueryableRecords.ToList();
        orderDetailList.ForEach(orderDetail =>
        {
            orderDetail.CurrencyTitle = orderDetail.Currency > 0 ? EnumExtensions.GetEnumTitle<Currency>(orderDetail.Currency) : string.Empty;
        });
        return orderDetailList == null || orderDetailList.Count <= 0 ?
            new DataResult<List<OrderDetailDTO>>(ResultStatus.Error, new List<OrderDetailDTO>()) :
            new DataResult<List<OrderDetailDTO>>(ResultStatus.Success, orderDetailList);
    }

    public async Task<IDataResult<OrderDetail>> GetById(int id)
    {
        var orderDetail = await _UnitOfWork.OrderDetails.GetByIdAsync(id);
        return orderDetail == null || orderDetail.Id <= 0 ?
            new DataResult<OrderDetail>(ResultStatus.Error, Messages.ResultIsNotFound, new OrderDetail()) :
            new DataResult<OrderDetail>(ResultStatus.Success, orderDetail);
    }

    public async Task<IResult> InsertAsync(OrderDetail entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.OrderDetails.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(OrderDetail entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.OrderDetails.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }
}
