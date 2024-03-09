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
public class HomeworkTransferAPIController : ControllerBase
{
    private readonly IHomeworkTransferService _homeworkTransferService;
    private readonly IMapper _mapper;
    private readonly IValidator<HomeworkTransferApiDTO> _homeworkTransferValidator;

    public HomeworkTransferAPIController(IHomeworkTransferService homeworkTransferService, IMapper mapper, IValidator<HomeworkTransferApiDTO> homeworkTransferValidator)
    {
        _homeworkTransferService = homeworkTransferService;
        _mapper = mapper;
        _homeworkTransferValidator = homeworkTransferValidator;
    }
    [AllowAnonymous]
    [HttpPost("AddOrUpdateHomeworkTransfer")]
    public async Task<IActionResult> AddOrUpdateHomeworkTransfer([FromForm] string key, [FromForm] HomeworkTransferApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _homeworkTransferValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var homeworkTransfer = _mapper.Map<HomeworkTransfer>(apiDTO);
        #region BURADAKI DEGERLERI KENDIMIZ ATAMAMIZ GEREKIYOR
        homeworkTransfer.TransferDate = DateTime.Now;
        var statusResult = await _homeworkTransferService.DetermineStatus(apiDTO.HomeworkId);
        homeworkTransfer.Status = statusResult.ResultStatus == ResultStatus.Success ? statusResult.Data : 0;
        #endregion
        var result = apiDTO.Id > 0 ? await _homeworkTransferService.UpdateOrDelete(homeworkTransfer) : await _homeworkTransferService.InsertAsync(homeworkTransfer);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<HomeworkTransfer>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<HomeworkTransfer>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteHomeworkTransfer")]
    public async Task<IActionResult> DeleteHomeworkTransfer([FromForm] string key, [FromForm] int homeworkTransferId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var homeworkTransferResult = await _homeworkTransferService.GetById(homeworkTransferId);
        var result = homeworkTransferResult.ResultStatus == ResultStatus.Success && homeworkTransferResult.Data.Id > 0 ?
            await _homeworkTransferService.UpdateOrDelete(homeworkTransferResult.Data) : new Result(ResultStatus.Error);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<HomeworkTransfer>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<HomeworkTransfer>())));
    }
}
