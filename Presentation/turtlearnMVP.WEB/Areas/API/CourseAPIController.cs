using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Configurations;
using turtlearnMVP.Persistance.Validations.Entities;

namespace turtlearnMVP.WEB.Areas.API
{
    [Route("turtlearnApi/[controller]")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseApiDTO> _courseValidator;

        public CourseAPIController(ICourseService courseService, IMapper mapper, IValidator<CourseApiDTO> courseValidator, ISessionService sessionService)
        {
            _courseService = courseService;
            _mapper = mapper;
            _courseValidator = courseValidator;
            _sessionService = sessionService;
        }
        [AllowAnonymous]
        [HttpPost("AddOrUpdateCourse")]
        public async Task<IActionResult> AddOrUpdateCourse([FromForm] string key, [FromForm] CourseApiDTO apiDTO)
        {
            var _Key = await turtlearnApiSetting.getKey();
            if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
            {
                return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
            }
            var validationResult = await _courseValidator.ValidateAsync(apiDTO);
            if (!validationResult.IsValid)
            {
                // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
                return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
            }
            var course = _mapper.Map<Course>(apiDTO);
            var result = apiDTO.Id > 0 ? await _courseService.UpdateOrDelete(course) : await _courseService.InsertAsync(course);
            return result.ResultStatus == ResultStatus.Success
                ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Course>())))
           : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Course>())));
        }
        [AllowAnonymous]
        [HttpPost("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse([FromForm] string key, [FromForm] int courseId)
        {
            var _Key = await turtlearnApiSetting.getKey();
            if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)) || courseId <= 0)
            {
                return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
            }
            var categoryResult = await _courseService.GetById(courseId);
            var result = categoryResult.ResultStatus == ResultStatus.Success && categoryResult.Data.Id > 0 ?
                await _courseService.UpdateOrDelete(categoryResult.Data) : new Result(ResultStatus.Error);
            return result.ResultStatus == ResultStatus.Success
                ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<Course>())))
           : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<Course>())));
        }
        [AllowAnonymous]
        [HttpPost("GetAllCourses")]
        public async Task<IActionResult> GetAllCouses([FromForm] string key)
        {
            var _Key = await turtlearnApiSetting.getKey();
            if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
            {
                return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
            }
            var courseListResult = _courseService.FetchAllDtos();
            return courseListResult.ResultStatus == ResultStatus.Success
               ? Ok(new ApiDataResult<IList<CourseDTO>>(_Key, ResultStatus.Success,
               Messages.SuccessDelete(TableExtensions.GetTableTitle<Course>()), courseListResult.Data))
          : BadRequest(new ApiDataResult<IList<CourseDTO>>(_Key, ResultStatus.Error,
          Messages.FailedDelete(TableExtensions.GetTableTitle<Course>()), courseListResult.Data));
        }
        //[AllowAnonymous]
        //[HttpPost("GetCourseDetails")]
        //public async Task<IActionResult> GetCourseDetails([FromForm] string key, [FromForm] int courseId)
        //{
        //    var _Key = await turtlearnApiSetting.getKey();
        //    if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        //    {
        //        return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        //    }
        //    var courseResult = await _courseService.GetById(courseId);
        //    var apiDTO = 
            
        //}
    }
}
