using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services;

public interface IOrderDetailService
{
    IQueryable<OrderDetailDTO> _QueryableOrderDetails { get; }
    Task<IResult> InsertAsync(OrderDetail entity);
    Task<IResult> UpdateOrDelete(OrderDetail entity);
    IDataResult<IList<OrderDetailDTO>> FetchAllDtos();
    Task<IDataResult<OrderDetail>> GetById(int id);
}
