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

public class EfOfferRepository : Repository<Offer>, IOfferRepository
{
    private readonly turtlearnMVPContext _Context;
    public EfOfferRepository(DbContext context) : base(context)
    => _Context = context as turtlearnMVPContext;

    public IQueryable<OfferDTO> GetAllQueryableRecords()
    {
        var Query = from O in _Context.Offers
                    select new OfferDTO
                    {
                        Id = O.Id,
                        Name = O.Name,
                        Type = O.Type,
                        Code = O.Code,
                        DiscountRate = O.DiscountRate,
                        Description = O.Description,
                    };
        return Query;
    }
}
