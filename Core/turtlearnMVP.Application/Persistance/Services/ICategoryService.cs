using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Rendering;
using TurtLearn.Shared.Utilities.Results.Abstract;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.Application.Persistance.Services
{
    public interface ICategoryService
    {
        IQueryable<CategoryDTO> _QueryableCategories { get; }
        Task<IResult> InsertAsync(Category entity);
        Task<IResult> UpdateOrDelete(Category entity);
        IDataResult<IList<CategoryDTO>> FetchAllDtos();
        Task<IDataResult<Category>> GetById(int id);
        //SelectList GetAsSelectList();
    }
}
