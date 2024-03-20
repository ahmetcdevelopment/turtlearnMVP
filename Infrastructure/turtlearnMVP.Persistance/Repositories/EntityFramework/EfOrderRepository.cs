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

public class EfOrderRepository : Repository<Order>, IOrderRepository
{
    private readonly turtlearnMVPContext _Context;
    public EfOrderRepository(DbContext context) : base(context)
    => _Context = context as turtlearnMVPContext;

    public IQueryable<OrderDTO> GetAllQueryableRecords()
    {
        var Query = from O in _Context.Orders
                    join U in _Context.Users on O.UserId equals U.Id
                    select new OrderDTO
                    {
                        Id = O.Id,
                        UserId = O.UserId,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email,
                        PhoneNumber = U.PhoneNumber,
                        Status = O.Status,
                        Date = O.Date,
                        AmountPaid = O.AmountPaid,
                        EmailConfirmCode = O.EmailConfirmCode,
                    };
        return Query;
    }
}
