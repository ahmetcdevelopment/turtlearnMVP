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

public class EfOfferDetailRepository : Repository<OfferDetail>, IOfferDetailRepository
{
    private readonly turtlearnMVPContext _Context;
    public EfOfferDetailRepository(DbContext context) : base(context)
    => _Context = context as turtlearnMVPContext;

    public IQueryable<OfferDetailDTO> GetAllQueryableRecords()
    {
        var Query = from OD in _Context.OfferDetails
                    join O in _Context.Offers on OD.OfferId equals O.Id into offerJoin
                    from O in offerJoin.DefaultIfEmpty()
                    join C in _Context.Courses on OD.CourseId equals C.Id into courseJoin
                    from C in courseJoin.DefaultIfEmpty()
                    select new OfferDetailDTO
                    {
                        Id = OD.Id,
                        OfferId = O.Id,
                        OfferName = O.Name,
                        CourseId = C.Id,
                        CourseName = C.Name,
                        DefinitionDate = OD.DefinitionDate,
                        DiscountRate = O.DiscountRate,
                        EndDate = OD.EndDate,
                    };
        return Query;
    }
}
