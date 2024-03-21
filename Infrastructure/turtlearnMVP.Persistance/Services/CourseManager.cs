using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;

namespace turtlearnMVP.Persistance.Services
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CourseManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        private static string _tableNameTR = TableExtensions.GetTableTitle<Course>();

        public IQueryable<CourseDTO> _QueryableCourses => _UnitOfWork.Courses.GetAllQueryableRecords();

        //public IDataResult<IList<CourseDTO>> FetchAllDtos()
        //{

        //    var allQueryableRecords = _UnitOfWork.Courses.GetAllQueryableRecords();
        //    var CourseList = allQueryableRecords.ToList();
        //    CourseList.ForEach(course =>
        //    {
        //        course.StatusTitle = course.Status > 0 ? EnumExtensions.GetEnumTitle<CourseStatus>((int)course.Status) : string.Empty;
        //    });
        //    return CourseList == null || CourseList.Count <= 0 ?
        //        new DataResult<List<CourseDTO>>(ResultStatus.Error, new List<CourseDTO>()) :
        //        new DataResult<List<CourseDTO>>(ResultStatus.Success, CourseList);
        //}

        public IDataResult<IList<CourseDTO>> FetchAllDtos(Expression<Func<CourseDTO, bool>> filter = null)
        {
            var allQueryableRecords = _UnitOfWork.Courses.GetAllQueryableRecords();

            // Eğer bir filtre varsa, veriyi filtrele
            if (filter != null)
            {
                allQueryableRecords = allQueryableRecords.Where(filter);
            }

            var courseList = allQueryableRecords.ToList();
            courseList.ForEach(course =>
            {
                course.StatusTitle = course.Status > 0 ? EnumExtensions.GetEnumTitle<CourseStatus>((int)course.Status) : string.Empty;
            });

            return courseList == null || courseList.Count <= 0 ?
                new DataResult<List<CourseDTO>>(ResultStatus.Error, new List<CourseDTO>()) :
                new DataResult<List<CourseDTO>>(ResultStatus.Success, courseList);
        }

        public async Task<IDataResult<Course>> GetById(int id)
        {
            var course = await _UnitOfWork.Courses.GetByIdAsync(id);
            return course == null || course.Id <= 0 ?
                new DataResult<Course>(ResultStatus.Error, Messages.ResultIsNotFound, new Course()) :
                new DataResult<Course>(ResultStatus.Success, course);
        }

        public async Task<IResult> InsertAsync(Course entity)
        {
            var message = Messages.FailedAdd(_tableNameTR);
            if (entity != null || entity.Id < 0)
            {
                await _UnitOfWork.Courses.AddAsync(entity);
                message = Messages.SuccessAdd(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IResult> UpdateOrDelete(Course entity)
        {
            var message = Messages.FailedUpdate(_tableNameTR);
            if (entity != null || entity.Id > 0)
            {
                await _UnitOfWork.Courses.UpdateAsync(entity);
                message = Messages.SuccessUpdate(_tableNameTR);
                await _UnitOfWork.SaveChanges();
                return new Result(ResultStatus.Success, message);
            }
            return new Result(ResultStatus.Error, message);
        }

        public async Task<IDataResult<CourseDetailApiDTO>> GetCourseDetailApiDTO(int courseId)
        => courseId > 0 ? new DataResult<CourseDetailApiDTO>(ResultStatus.Success, await _UnitOfWork.Courses.GetCourseDetailsAsync(courseId))
            : new DataResult<CourseDetailApiDTO>(ResultStatus.Error, $"Aradığınız kurs mevcut değil", null);
    }
}
