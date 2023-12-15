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


namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController
            (
                ICategoryService categoryService
            )
        {
            _categoryService = categoryService;
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
            if (id.HasValue)
            {
                var category = (await _categoryService.GetById(id.Value)).Data ?? new Category();
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
                var result = await _categoryService.GetById(model.Id.Value);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.SinifDuzeyiId = model.SinifDuzeyiId;
                result.Data.Content = model.Content;
                result.Data.Name = model.Name;
                result.Data.Description = model.Description;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ? await _categoryService.InsertAsync(result.Data) : await _categoryService.UpdateOrDelete(result.Data);
                return Json(new { Result = result });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }

        public JsonResult GetAllToGrid()
        {
            var data = _categoryService.FetchAllDtos();
            return Json( data );
        }
    }
}
