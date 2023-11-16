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
    public class HomeworkManager : IHomeworkService
    {
        public IQueryable<HomeworkDTO> _QueryableHomeworks => throw new NotImplementedException();

        public IDataResult<IList<HomeworkDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Homework>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(Homework entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(Homework entity)
        {
            throw new NotImplementedException();
        }
    }
}
