using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;


namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _mainService;
        public CategoryController
            (
                ICategoryService mainService
            )
        {
            _mainService = mainService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new CategoryEditViewModel();
            model.SelSinifDuzeyi = EnumHelper.GetEnumSelectList<SinifDuzeyi>();
            if (id.HasValue && id.Value > 0)
            {
                var category = (await _mainService.GetById(id.Value)).Data ?? new Category();
                model.Id = category.Id;
                model.SinifDuzeyiId = category.SinifDuzeyiId;
                model.Content = category.Content;
                model.Name = category.Name;
                model.Description = category.Description;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(CategoryEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mainService.GetById(model.Id.HasValue?model.Id.Value:0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.SinifDuzeyiId = model.SinifDuzeyiId;
                result.Data.Content = model.Content;
                result.Data.Name = model.Name;
                result.Data.Description = model.Description;
                result.Data.Picture = "~/images/avatar/default.jpg";
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ? await _mainService.UpdateOrDelete(result.Data) : await _mainService.InsertAsync(result.Data) ;
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }

        public JsonResult GetAllToGrid()
        {
            var data = _mainService.FetchAllDtos().Data;
            return Json( data, new Newtonsoft.Json.JsonSerializerSettings());
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

            if (result.ResultStatus == ResultStatus.Success)
            {
                var categoryList = result.Data;
                var selectListItems = categoryList
                    .Select(category => new SelectListItem
                    {
                        Text = category.Name,
                        Value = category.Id.ToString()
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
