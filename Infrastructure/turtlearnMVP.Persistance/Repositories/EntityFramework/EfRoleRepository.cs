using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Application.Persistance.Repositories;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Persistance.Context;

namespace turtlearnMVP.Persistance.Repositories.EntityFramework
{
    public class EfRoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly turtlearnMVPContext _Context;
        public EfRoleRepository(DbContext context) : base(context)
        => _Context = context as turtlearnMVPContext;

        public IQueryable<RoleDTO> GetAllQueryableRecords()
        {
            var Query = from R in _Context.Roles
                        select new RoleDTO
                        {
                            Id = R.Id,
                            Name = R.Name,
                        };
            return Query;
        }
    }
}
