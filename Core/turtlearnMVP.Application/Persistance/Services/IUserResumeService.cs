using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services;

public interface IUserResumeService
{
    IQueryable<UserResumeDTO> _QueryableResumes { get; }
    Task<IResult> InsertAsync(UserResume entity);
    Task<IResult> UpdateOrDelete(UserResume entity);
    IDataResult<IList<UserResumeDTO>> FetchAllDtos();
    Task<IDataResult<UserResume>> GetById(int id);
}
