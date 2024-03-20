using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services;

public interface IOrderService
{
    IQueryable<OrderDTO> _QueryableOrders { get; }
    Task<IResult> InsertAsync(Order entity);
    Task<IResult> UpdateOrDelete(Order entity);
    IDataResult<IList<OrderDTO>> FetchAllDtos();
    Task<IDataResult<Order>> GetById(int id);
}
