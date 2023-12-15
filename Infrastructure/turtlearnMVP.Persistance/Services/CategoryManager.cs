using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Searching;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.QueryDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.Persistance.Repositories;

namespace turtlearnMVP.Persistance.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ISearch<CategoryDTO> _Search;
        public CategoryManager(IUnitOfWork unitOfWork, ISearch<CategoryDTO> search)
        {
            _UnitOfWork = unitOfWork;
            _Search = search;
        }

        private static string _tableNameTR = TableExtensions.GetTableTitle<Category>();

        public IQueryable<CategoryDTO> _QueryableCategories => _UnitOfWork.Categories.GetAllQueryableRecords();

        public IDataResult<IList<CategoryDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Categories.GetAllQueryableRecords();
            var CategoryList = allQueryableRecords.ToList();
            CategoryList.ForEach(category =>
            {
                category.SinifDuzeyiTitle = category.SinifDuzeyiId > 0 && category.SinifDuzeyiId.HasValue ?
                EnumExtensions.GetEnumTitle<SinifDuzeyi>(category.SinifDuzeyiId.Value) : string.Empty;
            });
            return CategoryList == null || CategoryList.Count <= 0 ?
                new DataResult<List<CategoryDTO>>(ResultStatus.Error, new List<CategoryDTO>()) :
                new DataResult<List<CategoryDTO>>(ResultStatus.Success, CategoryList);
        }

        public async Task<IDataResult<Category>> GetById(int id)
        {
            var category = await _UnitOfWork.Categories.GetByIdAsync(id);
            return category == null || category.Id <= 0 ?
                new DataResult<Category>(ResultStatus.Error, Messages.ResultIsNotFound, new Category()) :
                new DataResult<Category>(ResultStatus.Success, category);
        }

        public async Task<IResult> InsertAsync(Category entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Categories.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Category entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Categories.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        //public async Task<System.Web.Mvc.Rendering.SelectList> getSelectList(CategoryDTO? categoryDTO)
        //{
        //    var categoryList = categoryDTO == null ? _QueryableCategories
        //        : new CategoryQueryDTO(_Search).GetFilteredData(_QueryableCategories, categoryDTO);
        //    return new System.Web.Mvc.Rendering.SelectList(await categoryList.Select((CategoryDTO item) => new SelectListItem
        //    {
        //        Text = string.Concat(item.Name),
        //        Value = item.Id.ToString()
        //    }).ToListAsync(), "Value", "Text");
        //}
    }
}
