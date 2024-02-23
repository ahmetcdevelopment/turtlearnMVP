using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Persistance.Configurations;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class CategoryAPIController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CategoryApiDTO> _categoryValidator;
    private readonly IMapper _mapper;

    public CategoryAPIController(ICategoryService categoryService, IValidator<CategoryApiDTO> categoryValidator)
    {
        _categoryService = categoryService;
        _categoryValidator = categoryValidator;
    }

    [AllowAnonymous]
    [HttpPost("AddOrUpdateCategory")]
    public async Task<IActionResult> AddOrUpdateCategory([FromForm] string key, [FromForm] CategoryApiDTO apiDTO)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var validationResult = await _categoryValidator.ValidateAsync(apiDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var category = _mapper.Map<Category>(apiDTO);
        var result = apiDTO.Id > 0 ? await _categoryService.UpdateOrDelete(category) : await _categoryService.InsertAsync(category);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<Category>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<Category>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteCategory")]
    public async Task<IActionResult> DeleteCategory([FromForm] string key, [FromForm] int categoryId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)) || categoryId <= 0)
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var categoryResult = await _categoryService.GetById(categoryId);
        var result = categoryResult.ResultStatus == ResultStatus.Success && categoryResult.Data.Id > 0 ?
            await _categoryService.UpdateOrDelete(categoryResult.Data) : new Result(ResultStatus.Error);
        return result.ResultStatus == ResultStatus.Success
            ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<Category>())))
       : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<Category>())));
    }
    [AllowAnonymous]
    [HttpPost("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories([FromForm] string key)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var categoryListResult = _categoryService.FetchAllDtos();
        return categoryListResult.ResultStatus == ResultStatus.Success
           ? Ok(new ApiDataResult<IList<CategoryDTO>>(_Key, ResultStatus.Success,
           Messages.SuccessDelete(TableExtensions.GetTableTitle<Category>()), categoryListResult.Data))
      : BadRequest(new ApiDataResult<IList<CategoryDTO>>(_Key, ResultStatus.Error,
      Messages.FailedDelete(TableExtensions.GetTableTitle<Category>()), categoryListResult.Data));
    }
    /// <summary>
    /// İleride yukarıdaki metod ile birleşip parametreye bağlanmalı.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("GetAllCategoryPairs")]
    public async Task<IActionResult> GetAllCategoryPairs([FromForm] string key)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var categoryListResult = _categoryService.FetchAllDtos();
        var categories = categoryListResult.Data.Select(x => new DropdownDTO<int?, string> { Key = x.Id, Value = x.Name }).ToList();
        return categoryListResult.ResultStatus == ResultStatus.Success
           ? Ok(new ApiDataResult<List<DropdownDTO<int?, string>>>(_Key, ResultStatus.Success,
           Messages.SuccessDelete(TableExtensions.GetTableTitle<Category>()), categories))
      : BadRequest(new ApiDataResult<List<DropdownDTO<int?, string>>>(_Key, ResultStatus.Error,
      Messages.FailedDelete(TableExtensions.GetTableTitle<Category>()), categories));
    }
}
