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
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Configurations;
using turtlearnMVP.Persistance.Validations.Entities;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class HomeworkAPIController : ControllerBase
{
    private readonly IHomeworkService _homeworkService;
    private readonly IValidator<HomeworkApiDTO> _homeworkValidator;
    private readonly IMapper _mapper;

    public HomeworkAPIController(IHomeworkService homeworkService, IValidator<HomeworkApiDTO> homeworkValidator, IMapper mapper)
    {
        _homeworkService = homeworkService;
        _homeworkValidator = homeworkValidator;
        _mapper = mapper;
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
}
