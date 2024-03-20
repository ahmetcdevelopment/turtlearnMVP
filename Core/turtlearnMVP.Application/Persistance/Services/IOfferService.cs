using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services;

public interface IOfferService
{
    IQueryable<OfferDTO> _QueryableOffers { get; }
    Task<IResult> InsertAsync(Offer entity);
    Task<IResult> UpdateOrDelete(Offer entity);
    IDataResult<IList<OfferDTO>> FetchAllDtos();
    Task<IDataResult<Offer>> GetById(int id);
}
