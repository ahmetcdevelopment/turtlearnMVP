using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface IHomeworkService
    {
        IQueryable<HomeworkDTO> _QueryableHomeworks { get; }
        Task<IResult> InsertAsync(Homework entity);
        Task<IResult> UpdateOrDelete(Homework entity);
        IDataResult<IList<HomeworkDTO>> FetchAllDtos();
        Task<IDataResult<Homework>> GetById(int id);
    }
}
