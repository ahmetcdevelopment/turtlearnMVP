using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
//using turtlearnMVP.WEB.Search;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class HomeworkAPIController : ControllerBase
{
    private readonly IHomeworkService _homeworkService;
    private readonly IHomeworkTransferService _homeworkTransferService;
    private readonly IValidator<HomeworkApiDTO> _homeworkValidator;
    private readonly IMapper _mapper;

    public HomeworkAPIController(IHomeworkService homeworkService, IValidator<HomeworkApiDTO> homeworkValidator, IMapper mapper
        , IHomeworkTransferService homeworkTransferService)
    {
        _homeworkService = homeworkService;
        _homeworkValidator = homeworkValidator;
        _mapper = mapper;
        _homeworkTransferService = homeworkTransferService;
    }

    [AllowAnonymous]
    [HttpPost("AddOrUpdateHomeworkToSession")]
    public async Task<IActionResult> AddOrUpdateHomeworkToSession([FromForm] string key, [FromForm] HomeworkApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _homeworkValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var homework = _mapper.Map<Homework>(apiDTO);
        var result = apiDTO.Id > 0 ? await _homeworkService.UpdateOrDelete(homework) : await _homeworkService.InsertAsync(homework);
        return result.ResultStatus == ResultStatus.Success
           ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Homework>())))
      : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Homework>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteHomework")]
    public async Task<IActionResult> DeleteHomework([FromForm] string key, [FromForm] int homeworkId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var homeworkResult = await _homeworkService.GetById(homeworkId);
        var result = homeworkResult.ResultStatus == ResultStatus.Success && homeworkResult.Data.Id > 0 ?
            await _homeworkService.UpdateOrDelete(homeworkResult.Data) : new Result(ResultStatus.Error);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<Session>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<Session>())));
    }
    [AllowAnonymous]
    [HttpPost("GetAllHomework")]
    public async Task<IActionResult> GetAllHomework([FromForm] string key, [FromForm] int sessionId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var homeworkQuery = _homeworkService._QueryableHomeworks.Where(x => x.SessionId == sessionId);
        var homeworkListResult = sessionId <= 0 || homeworkQuery == null ?
            new DataResult<List<HomeworkDTO>>(ResultStatus.Error, Messages.PageIsNotFound, null)
            : new DataResult<List<HomeworkDTO>>(ResultStatus.Success, await homeworkQuery.ToListAsync());
        return homeworkListResult.ResultStatus == ResultStatus.Success
           ? Ok(new ApiDataResult<List<HomeworkDTO>>(_Key, ResultStatus.Success,
           Messages.SuccessDelete(TableExtensions.GetTableTitle<Homework>()), homeworkListResult.Data))
      : BadRequest(new ApiDataResult<List<HomeworkDTO>>(_Key, ResultStatus.Error,
      Messages.FailedDelete(TableExtensions.GetTableTitle<Homework>()), homeworkListResult.Data));
    }
    [AllowAnonymous]
    [HttpPost("GetAllHomeworkTransfers")]
    public async Task<IActionResult> GetAllHomeworkTransfers([FromForm] string key, [FromForm] int homeworkId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var homeworkTransfers = _homeworkTransferService._QueryableHomeworkTransfers.Where(x => x.HomeworkId == homeworkId);
        var result = homeworkTransfers.Count() > 0 && homeworkId > 0 ?
            new DataResult<List<HomeworkTransferDTO>>(ResultStatus.Success, await homeworkTransfers.ToListAsync()) :
            new DataResult<List<HomeworkTransferDTO>>(ResultStatus.Error, Messages.ResultIsNotFound
            , await homeworkTransfers.ToListAsync() ?? new List<HomeworkTransferDTO>());
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiDataResult<List<HomeworkTransferDTO>>(_Key, ResultStatus.Success, result.Data))
       : BadRequest(new ApiDataResult<List<HomeworkTransferDTO>>(_Key, ResultStatus.Error, result.Message, null));
    }
}
