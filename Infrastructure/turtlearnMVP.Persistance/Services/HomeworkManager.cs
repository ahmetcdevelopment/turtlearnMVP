using System;
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

namespace turtlearnMVP.Persistance.Services
{
    public class HomeworkManager : IHomeworkService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public HomeworkManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<Homework>();

        public IQueryable<HomeworkDTO> _QueryableHomeworks => _UnitOfWork.Homeworks.GetAllQueryableRecords();

        public IDataResult<IList<HomeworkDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.Homeworks.GetAllQueryableRecords();
            var HomeworkList = allQueryableRecords.ToList();
            return HomeworkList == null || HomeworkList.Count <= 0 ?
                new DataResult<List<HomeworkDTO>>(ResultStatus.Error, new List<HomeworkDTO>()) :
                new DataResult<List<HomeworkDTO>>(ResultStatus.Success, HomeworkList);
        }

        public async Task<IDataResult<Homework>> GetById(int id)
        {
            var homework = await _UnitOfWork.Homeworks.GetByIdAsync(id);
            return homework == null || homework.Id <= 0 ?
                new DataResult<Homework>(ResultStatus.Error, Messages.ResultIsNotFound, homework) :
                new DataResult<Homework>(ResultStatus.Success, homework);
        }

        public async Task<IResult> InsertAsync(Homework entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Homeworks.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Homework entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Homeworks.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
