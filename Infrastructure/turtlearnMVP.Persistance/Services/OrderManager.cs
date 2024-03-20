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

public class OrderManager : IOrderService
{
    private readonly IUnitOfWork _UnitOfWork;

    public OrderManager(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    private static string _tableNameTR = TableExtensions.GetTableTitle<Order>();

    public IQueryable<OrderDTO> _QueryableOrders => _UnitOfWork.Orders.GetAllQueryableRecords();

    public IDataResult<IList<OrderDTO>> FetchAllDtos()
    {
        var allQueryableRecords = _UnitOfWork.Orders.GetAllQueryableRecords();
        var orderList = allQueryableRecords.ToList();
        orderList.ForEach(order =>
        {
            order.StatusTitle = order.Status > 0 ? EnumExtensions.GetEnumTitle<OrderStatus>(order.Status) : string.Empty;
        });
        return orderList == null || orderList.Count <= 0 ?
            new DataResult<List<OrderDTO>>(ResultStatus.Error, new List<OrderDTO>()) :
            new DataResult<List<OrderDTO>>(ResultStatus.Success, orderList);
    }

    public async Task<IDataResult<Order>> GetById(int id)
    {
        var order = await _UnitOfWork.Orders.GetByIdAsync(id);
        return order == null || order.Id <= 0 ?
            new DataResult<Order>(ResultStatus.Error, Messages.ResultIsNotFound, new Order()) :
            new DataResult<Order>(ResultStatus.Success, order);
    }

    public async Task<IResult> InsertAsync(Order entity)
    {
        var message = Messages.FailedAdd(_tableNameTR);
        if (entity != null || entity.Id < 0)
        {
            await _UnitOfWork.Orders.AddAsync(entity);
            message = Messages.SuccessAdd(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }

    public async Task<IResult> UpdateOrDelete(Order entity)
    {
        var message = Messages.FailedUpdate(_tableNameTR);
        if (entity != null || entity.Id > 0)
        {
            await _UnitOfWork.Orders.UpdateAsync(entity);
            message = Messages.SuccessUpdate(_tableNameTR);
            await _UnitOfWork.SaveChanges();
            return new Result(ResultStatus.Success, message);
        }
        return new Result(ResultStatus.Error, message);
    }
}
