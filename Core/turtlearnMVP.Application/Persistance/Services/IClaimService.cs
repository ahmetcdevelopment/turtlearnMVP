using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface IClaimService
    {
        IQueryable<ClaimDTO> _QueryableClaims { get; }
        Task<IResult> InsertAsync(Claim entity);
        Task<IResult> UpdateOrDelete(Claim entity);
        IDataResult<IList<ClaimDTO>> FetchAllDtos();
        IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModules();
        IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModulesWithCheckeds(Role role);
        IDataResult<IList<ClaimsByModulesDTO>> FetchAllDtosByModulesWithCheckeds(User user);
        IDataResult<IList<ClaimDTO>> FetchAllDtosByIds(List<int> selectedIds);
        Task<IDataResult<Claim>> GetById(int id);
    }
}
