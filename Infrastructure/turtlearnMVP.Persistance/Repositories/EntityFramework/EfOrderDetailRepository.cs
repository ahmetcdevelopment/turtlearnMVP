using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework;

public class EfOrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly turtlearnMVPContext _Context;
    public EfOrderDetailRepository(DbContext context) : base(context)
    => _Context = context as turtlearnMVPContext;

    public IQueryable<OrderDetailDTO> GetAllQueryableRecords()
    {
        var Query = from OD in _Context.OrderDetails
                    join O in _Context.Orders.DefaultIfEmpty() on OD.OrderId equals O.Id
                    join C in _Context.Courses.DefaultIfEmpty() on OD.CourseId equals C.Id
                    select new OrderDetailDTO
                    {
                        Id = OD.Id,
                        OrderId = O.Id,
                        CourseId = C.Id,
                        Name = C.Name,
                        DiscountRate = OD.DiscountRate,
                        Amount = OD.Amount,
                        Currency = OD.Currency,
                        OfferCode = OD.OfferCode,
                        TaxPrice = OD.TaxPrice,
                        TaxRate = OD.TaxRate,
                    };
        return Query;
    }
}
