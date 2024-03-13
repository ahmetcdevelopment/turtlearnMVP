using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.DataAccess.Repositories;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.Application.Persistance.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IQueryable<RoleDTO> GetAllQueryableRecords();
    }
}
