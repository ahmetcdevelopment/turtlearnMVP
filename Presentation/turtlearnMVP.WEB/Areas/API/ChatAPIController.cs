using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class ChatAPIController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IChatUserService _chatUserService;
    private readonly IMapper _mapper;
    private readonly IValidator<ChatApiDTO> _chatValidator;

    public ChatAPIController(IChatService chatService, IMapper mapper, IValidator<ChatApiDTO> chatValidator, IChatUserService chatUserService)
    {
        _chatService = chatService;
        _mapper = mapper;
        _chatValidator = chatValidator;
        _chatUserService = chatUserService;
    }
    [AllowAnonymous]
    [HttpPost("AddOrUpdateChat")]
    public async Task<IActionResult> AddOrUpdateChat([FromForm] string key, [FromForm] ChatApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _chatValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var chat = _mapper.Map<Chat>(apiDTO);
        var result = apiDTO.Id > 0 ? await _chatService.UpdateOrDelete(chat) : await _chatService.InsertAsync(chat);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Chat>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Chat>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteChat")]
    public async Task<IActionResult> DeleteChat([FromForm] string key, [FromForm] int chatId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)) || chatId <= 0)
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var chatResult = await _chatService.GetById(chatId);
        var result = chatResult.ResultStatus == ResultStatus.Success && chatResult.Data.Id > 0 ?
            await _chatService.UpdateOrDelete(chatResult.Data) : new Result(ResultStatus.Error);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<Chat>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<Chat>())));
    }
    [AllowAnonymous]
    [HttpPost("GetAllChats")]
    public async Task<IActionResult> GetAllChats([FromForm] string key)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var chatListResult = _chatService.FetchAllDtos();
        return chatListResult.ResultStatus == ResultStatus.Success
           ? Ok(new ApiDataResult<IList<ChatDTO>>(_Key, ResultStatus.Success,
           Messages.SuccessDelete(TableExtensions.GetTableTitle<Chat>()), chatListResult.Data))
      : BadRequest(new ApiDataResult<IList<ChatDTO>>(_Key, ResultStatus.Error,
      Messages.FailedDelete(TableExtensions.GetTableTitle<Chat>()), chatListResult.Data));
    }
    [AllowAnonymous]
    [HttpPost("GetChatUsers")]
    public async Task<IActionResult> GetChatUsers([FromForm] string key, [FromForm] int chatId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)) || chatId <= 0)
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var chatUsers = await _chatUserService._QueryableChatUsers
            .Where(x => x.ChatId == chatId)
            .ToListAsync();
        return chatUsers != null || chatUsers?.Count > 0 ?
            Ok(new ApiDataResult<List<ChatUserDTO>>(key, ResultStatus.Success, chatUsers)) :
            BadRequest(new ApiDataResult<List<ChatUserDTO>>(key, ResultStatus.Error, chatUsers ?? new List<ChatUserDTO>()));
    }
}
