using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface IZrfRoleService
    {
        IDataResult<IList<RoleDTO>> FetchAllDtos();
        IDataResult<IList<RoleDTO>> FetchAllDtosWithCheckeds(User user);
        IDataResult<IList<RoleDTO>> FetchAllDtosByIds(List<int> selectedIds);
    }
}
