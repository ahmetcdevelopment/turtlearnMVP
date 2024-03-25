using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class OfferController : Controller
{
    private readonly IOfferService _offerService;
    private static string _code = "";
    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> AddOrUpdate(int? id)
    {
        var model = new OfferEditViewModel();
        model.SelTypes = EnumHelper.GetEnumSelectList<OfferType>();
        _code = Guid.NewGuid().ToString().Substring(0, 20);
        if (id.HasValue && id.Value > 0)
        {
            var entity = (await _offerService.GetById(id.Value)).Data ?? new Offer();
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.DiscountRate = entity.DiscountRate;
            model.Type = entity.Type;
        }
        model.Code = _code;

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(OfferEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _offerService.GetById(model.Id > 0 ? model.Id : 0);
            if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
            {
                return Json(new { Result = result });
            }
            result.Data.Name = model.Name;
            result.Data.Code = _code;
            result.Data.Description = model.Description;
            result.Data.DiscountRate = model.DiscountRate;
            result.Data.Type = model.Type;
            if (result.Data.Id == 0)
            {
                result.Data.IsDeleted = false;
                result.Data.UpdateDate = DateTime.Now;
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
            }
            var addOrUpdateResult = result.Data.Id > 0 ? await _offerService.UpdateOrDelete(result.Data) : await _offerService.InsertAsync(result.Data);

            return Json(new { Result = addOrUpdateResult });
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
    }
    public async Task<JsonResult> DeleteOffer(int? id)
    {
        if (id.HasValue)
        {
            var result = await _offerService.GetById(id.Value);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return Json(new { Result = result });
            }
            result.Data.IsDeleted = true;
            var updateResut = await _offerService.UpdateOrDelete(result.Data);
            return Json(new { Result = updateResut });
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
    }
    public async Task<JsonResult> GetAllToGrid()
    {
        var result = _offerService.FetchAllDtos().Data;
        return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
    }
}
