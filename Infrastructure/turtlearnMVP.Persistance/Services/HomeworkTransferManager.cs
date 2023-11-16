using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Persistance.Services
{
    public class HomeworkTransferManager : IHomeworkTransferService
    {
        public IQueryable<HomeworkTransferDTO> _QueryableHomeworkTransfers => throw new NotImplementedException();

        public IDataResult<IList<HomeworkTransferDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<HomeworkTransfer>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(HomeworkTransfer entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(HomeworkTransfer entity)
        {
            throw new NotImplementedException();
        }
    }
}
