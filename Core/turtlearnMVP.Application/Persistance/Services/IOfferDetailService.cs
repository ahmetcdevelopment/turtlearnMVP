using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services;

public interface IOfferDetailService
{
    IQueryable<OfferDetailDTO> _QueryableOfferDetails { get; }
    Task<IResult> InsertAsync(OfferDetail entity);
    Task<IResult> UpdateOrDelete(OfferDetail entity);
    IDataResult<IList<OfferDetailDTO>> FetchAllDtos();
    Task<IDataResult<OfferDetail>> GetById(int id);
}
