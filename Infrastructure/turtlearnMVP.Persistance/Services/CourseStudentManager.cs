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
    public class CourseStudentManager : ICourseStudentService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CourseStudentManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<CourseStudent>();

        public IQueryable<CourseStudentDTO> _QueryableCourseStudents => _UnitOfWork.CoursesStudents.GetAllQueryableRecords();

        public IDataResult<IList<CourseStudentDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.CoursesStudents.GetAllQueryableRecords();
            var CourseStudentList = allQueryableRecords.ToList();
            return CourseStudentList == null || CourseStudentList.Count <= 0 ?
                new DataResult<List<CourseStudentDTO>>(ResultStatus.Error, new List<CourseStudentDTO>()) :
                new DataResult<List<CourseStudentDTO>>(ResultStatus.Success, CourseStudentList);
        }

        public async Task<IDataResult<CourseStudent>> GetById(int id)
        {
            var courseStudent = await _UnitOfWork.CoursesStudents.GetByIdAsync(id);
            return courseStudent == null || courseStudent.Id <= 0 ?
                new DataResult<CourseStudent>(ResultStatus.Error, Messages.ResultIsNotFound, courseStudent) :
                new DataResult<CourseStudent>(ResultStatus.Success, courseStudent);
        }

        public async Task<IResult> InsertAsync(CourseStudent entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.CoursesStudents.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(CourseStudent entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.CoursesStudents.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
