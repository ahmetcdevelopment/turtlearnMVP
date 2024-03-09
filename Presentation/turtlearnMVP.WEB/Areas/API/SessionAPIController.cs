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
using turtlearnMVP.WEB.Search;
//using turtlearnMVP.WEB.Search;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class SessionAPIController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly IMapper _mapper;
    private readonly IValidator<SessionApiDTO> _sessionValidator;

    public SessionAPIController(ISessionService sessionService, IMapper mapper,
        IValidator<SessionApiDTO> sessionValidator)
    {
        _sessionService = sessionService;
        _mapper = mapper;
        _sessionValidator = sessionValidator;
    }
    [AllowAnonymous]
    [HttpPost("AddOrUpdateSession")]
    public async Task<IActionResult> AddOrUpdateSession([FromForm] string key, [FromForm] SessionApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _sessionValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var session = _mapper.Map<Session>(apiDTO);
        var result = apiDTO.Id > 0 ? await _sessionService.UpdateOrDelete(session) : await _sessionService.InsertAsync(session);
        return result.ResultStatus == ResultStatus.Success
           ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Session>())))
      : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Session>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteSession")]
    public async Task<IActionResult> DeleteSession([FromForm] string key, [FromForm] int sessionId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var sessionResult = await _sessionService.GetById(sessionId);
        var result = sessionResult.ResultStatus == ResultStatus.Success && sessionResult.Data.Id > 0 ?
            await _sessionService.UpdateOrDelete(sessionResult.Data) : new Result(ResultStatus.Error);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<Session>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<Session>())));
    }
    [AllowAnonymous]
    [HttpPost("GetAllSessions")]
    public async Task<IActionResult> GetAllSessions([FromForm] string key, [FromForm] int courseId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var sessionQuery = _sessionService._QueryableSessions.Where(x => x.CourseId == courseId);
        var sessionListResult = courseId <= 0 || sessionQuery == null ?
            new DataResult<List<SessionDTO>>(ResultStatus.Error, Messages.PageIsNotFound, null)
            : new DataResult<List<SessionDTO>>(ResultStatus.Success, await sessionQuery.ToListAsync());
        return sessionListResult.ResultStatus == ResultStatus.Success
           ? Ok(new ApiDataResult<List<SessionDTO>>(_Key, ResultStatus.Success,
           Messages.SuccessDelete(TableExtensions.GetTableTitle<Session>()), sessionListResult.Data))
      : BadRequest(new ApiDataResult<List<SessionDTO>>(_Key, ResultStatus.Error,
      Messages.FailedDelete(TableExtensions.GetTableTitle<Session>()), sessionListResult.Data));
    }
    [AllowAnonymous]
    [HttpPost("GetSessionDetails")]
    public async Task<IActionResult> GetSessionDetails([FromForm] string key, [FromForm] int sessionId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var result = await _sessionService.GetSessionApiDTO(sessionId);
        return result.ResultStatus == ResultStatus.Success && result.Data != null
            ? Ok(new ApiDataResult<SessionDetailApiDTO>(_Key, ResultStatus.Success, result.Data))
       : BadRequest(new ApiDataResult<SessionDetailApiDTO>(_Key, ResultStatus.Error,
       Messages.FailedUpdate(TableExtensions.GetTableTitle<Session>()), result.Data ?? new SessionDetailApiDTO()));
    }
}
