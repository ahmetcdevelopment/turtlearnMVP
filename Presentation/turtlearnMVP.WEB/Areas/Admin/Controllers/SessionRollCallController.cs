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
    public class SessionRollCallController : Controller
    {
        private readonly ISessionRollCallService _sessionRollCallService;

        public SessionRollCallController(ISessionRollCallService sessionRollCallService)
        {
            _sessionRollCallService = sessionRollCallService;
        }

        public async Task<IActionResult> Index()
        {
            //model şimdilik boş olsa da ileride doldurmak istersek diye hazır duruyor.
            var model = new SessionRollCallListViewModel();
            return View();
        }
        public async Task<IActionResult> GetAllToGrid()
        {
            var data = _sessionRollCallService.FetchAllDtos()?.Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new SessionRollCallEditViewModel();
            if (id.HasValue && id.Value > 0)
            {
                var resultData = (await _sessionRollCallService.GetById(id.Value)).Data ?? new SessionRollCall();
                // Assuming there is a GetCourseById method in _mainService
                model.Id = resultData.Id;
                model.CourseId = resultData.CourseId;
                model.SessionId = resultData.SessionId;
                model.UserId = resultData.UserId;
                model.TimeToJoin = resultData.TimeToJoin;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(SessionRollCallEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _sessionRollCallService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.CourseId = model.CourseId;
                model.SessionId = model.SessionId;
                model.UserId = model.SessionId;
                model.TimeToJoin = model.TimeToJoin;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ?
                    await _sessionRollCallService.UpdateOrDelete(result.Data) :
                    await _sessionRollCallService.InsertAsync(result.Data);
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _sessionRollCallService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _sessionRollCallService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }
    }
}
