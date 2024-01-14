using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class HomeworkTransferController : Controller
    {
        private readonly IHomeworkTransferService _homeworkTransferService;

        public HomeworkTransferController(IHomeworkTransferService homeworkTransferService)
        {
            _homeworkTransferService = homeworkTransferService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeworkTransferListViewModel();
            return View();
        }
        public async Task<JsonResult> GetAllToGrid()
        {
            var data = _homeworkTransferService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new HomeworkTransferEditViewModel();
            if (id.HasValue && id.Value > 0)
            {
                var resultData = (await _homeworkTransferService.GetById(id.Value)).Data ?? new HomeworkTransfer();
                // Assuming there is a GetCourseById method in _mainService
                model.Id = resultData.Id;
                model.StudentId = resultData.StudentId;
                model.Attachment = resultData.Attachment;
                model.Status = resultData.Status;
                model.Description = resultData.Description;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(HomeworkTransferEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _homeworkTransferService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.HomeworkId = model.HomeworkId;
                result.Data.StudentId = model.StudentId;
                result.Data.Attachment = model.Attachment;
                result.Data.Status = model.Status;
                result.Data.Description = model.Description;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ?
                    await _homeworkTransferService.UpdateOrDelete(result.Data) :
                    await _homeworkTransferService.InsertAsync(result.Data);
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _homeworkTransferService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _homeworkTransferService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }
    }
}
