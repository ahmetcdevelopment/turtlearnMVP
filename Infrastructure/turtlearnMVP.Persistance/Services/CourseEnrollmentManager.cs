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
    public class CourseEnrollmentManager : ICourseEnrollmentService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CourseEnrollmentManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        private static string _tableNameTR = TableExtensions.GetTableTitle<CourseEnrollment>();

        public IQueryable<CourseEnrollmentDTO> _QueryableCourseEnrollments => _UnitOfWork.CoursesEnrollments.GetAllQueryableRecords();

        public IDataResult<IList<CourseEnrollmentDTO>> FetchAllDtos()
        {
            var allQueryableRecords = _UnitOfWork.CoursesEnrollments.GetAllQueryableRecords();
            var CourseEnrollmentList = allQueryableRecords.ToList();
            return CourseEnrollmentList == null || CourseEnrollmentList.Count <= 0 ?
                new DataResult<List<CourseEnrollmentDTO>>(ResultStatus.Error, new List<CourseEnrollmentDTO>()) :
                new DataResult<List<CourseEnrollmentDTO>>(ResultStatus.Success, CourseEnrollmentList);
        }

        public async Task<IDataResult<CourseEnrollment>> GetById(int id)
        {
            var courseEnrollment = await _UnitOfWork.CoursesEnrollments.GetByIdAsync(id);
            return courseEnrollment == null || courseEnrollment.Id <= 0 ?
                new DataResult<CourseEnrollment>(ResultStatus.Error, Messages.ResultIsNotFound, courseEnrollment) :
                new DataResult<CourseEnrollment>(ResultStatus.Success, courseEnrollment);
        }

        public async Task<IResult> InsertAsync(CourseEnrollment entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.CoursesEnrollments.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(CourseEnrollment entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.CoursesEnrollments.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message); ;
        }

        public async Task<IResult> BulkInsert(List<CourseEnrollment> _entityList)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (_entityList != null || _entityList?.Count() < 0)
            {
                await _UnitOfWork.CoursesEnrollments.BulkInsert(_entityList);
                message = Messages.SuccessAdd(_tableNameTR);
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }
    }
}
