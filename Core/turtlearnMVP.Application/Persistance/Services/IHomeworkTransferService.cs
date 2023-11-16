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
    public interface IHomeworkTransferService
    {
        IQueryable<HomeworkTransferDTO> _QueryableHomeworkTransfers { get; }
        Task<IResult> InsertAsync(HomeworkTransfer entity);
        Task<IResult> UpdateOrDelete(HomeworkTransfer entity);
        IDataResult<IList<HomeworkTransferDTO>> FetchAllDtos();
        Task<IDataResult<HomeworkTransfer>> GetById(int id);
    }
}
