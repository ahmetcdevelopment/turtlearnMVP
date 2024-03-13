using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;
using Claim = turtlearnMVP.Domain.Entities.Claim;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClaimController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IClaimService _mainService;
        public ClaimController(IClaimService mainService, UserManager<User> userManager)
        {
            _mainService = mainService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new ClaimEditViewModel();
            var specialItems = new[]
            {
                (Value: 127, Text: "Özel Yetki"),
            };
            model.SelTableList = TableHelper.GetTablesSelectlistAllWithSpecial(specialItems);
            if (id.HasValue && id.Value > 0)
            {
                var main = (await _mainService.GetById(id.Value)).Data ?? new Claim();


                model.Id = main.Id;
                model.ClaimTypeId = main.ClaimTypeId;
                model.ClaimTypeStr = model.SelTableList.FirstOrDefault(x => x.Value == main.ClaimTypeId.ToString()).Text;
                model.ClaimValue = main.ClaimValue;

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(ClaimEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mainService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.ClaimTypeId = model.ClaimTypeId.Value;
                result.Data.ClaimValue = model.ClaimValue;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ? await _mainService.UpdateOrDelete(result.Data) : await _mainService.InsertAsync(result.Data);

                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }

        public JsonResult GetAllToGrid()
        {
            var data = _mainService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _mainService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _mainService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }

        public SelectList GetAsSelectList()
        {
            var result = _mainService.FetchAllDtos();
            var tables = TableHelper.GetTablesSeleclist();
            if (result.ResultStatus == ResultStatus.Success)
            {
                var claimList = result.Data;
                var selectListItems = claimList
                    .Select(claim => new SelectListItem
                    {
                        Text = tables.Where(x => x.Value == claim.ClaimTypeId.ToString()).FirstOrDefault().Text + "/" + claim.ClaimValue,
                        Value = claim.Id.ToString()
                    })
                    .ToList();
                var selectList = new SelectList(selectListItems, "Value", "Text");
                return selectList;
            }
            else
            {
                return null;
            }
        }
    }
}
