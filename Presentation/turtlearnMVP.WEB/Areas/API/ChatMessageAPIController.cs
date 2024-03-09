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
public class ChatMessageAPIController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;
    private readonly IValidator<MessageApiDTO> _messageValidator;

    public ChatMessageAPIController(IMessageService messageService, IMapper mapper, IValidator<MessageApiDTO> messageValidator)
    {
        _messageService = messageService;
        _mapper = mapper;
        _messageValidator = messageValidator;
    }
    [AllowAnonymous]
    [HttpPost("SendOrEditMessage")]
    public async Task<IActionResult> SendOrEditMessage([FromForm] string key, [FromForm] MessageApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _messageValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var message = _mapper.Map<Message>(apiDTO);
        var result = apiDTO.Id > 0 ? await _messageService.UpdateOrDelete(message) : await _messageService.InsertAsync(message);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Message>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Message>())));
    }
}