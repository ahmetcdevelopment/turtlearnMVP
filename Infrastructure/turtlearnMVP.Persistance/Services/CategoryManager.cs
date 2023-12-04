﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.Persistance.Repositories;

namespace turtlearnMVP.Persistance.Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
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
            var categoy = await _UnitOfWork.Categories.GetByIdAsync(id);
            return categoy == null || categoy.Id <= 0 ?
                new DataResult<Category>(ResultStatus.Error, Messages.ResultIsNotFound, categoy) :
                new DataResult<Category>(ResultStatus.Success, categoy);
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
    }
}
