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
    public class CategoryManager : ICategoryService
    {
        public IQueryable<CategoryDTO> _QueryableCategories => throw new NotImplementedException();

        public IDataResult<IList<CategoryDTO>> FetchAllDtos()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Category>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> InsertAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UpdateOrDelete(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
