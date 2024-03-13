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

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfClaimRepository :Repository<Claim>, IClaimRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfClaimRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<ClaimDTO> GetAllQueryableRecords()
        {
            var Query = from C in _Context.Claims
                        where C.IsDeleted == false
                        select new ClaimDTO
                        {
                            Id = C.Id,
                            ClaimTypeId = C.ClaimTypeId,
                            ClaimValue = C.ClaimValue,
                        };
            return Query;
        }
    }
}
