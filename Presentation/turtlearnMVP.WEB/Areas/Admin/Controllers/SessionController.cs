using Microsoft.AspNetCore.Http;
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
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new SessionListViewModel();
            return View();
        }
        public async Task<JsonResult> GetAllToGrid()
        {
            var data = _sessionService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id, int? courseId)
        {
            var model = new SessionEditViewModel();
            if (id.HasValue && id.Value > 0)
            {
                var resultData = (await _sessionService.GetById(id.Value)).Data ?? new Session(); 
                // Assuming there is a GetCourseById method in _mainService
                model.Id = resultData.Id;
                model.CourseId = resultData.CourseId;
                model.Name = resultData.Name;
                model.Description = resultData.Description;
                model.StartDate = resultData.StartDate;
                model.Link = resultData.Link;
            }
            else
            {
                model.CourseId = courseId.HasValue ? courseId.Value : 0;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(SessionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _sessionService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                int last = _sessionService.FetchAllDtos().Data.Where(x => x.CourseId == model.CourseId).OrderBy(x => x.Queue).LastOrDefault().Queue > 0 ? _sessionService.FetchAllDtos().Data.Where(x => x.CourseId == model.CourseId).OrderBy(x => x.Queue).LastOrDefault().Queue: 0;
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.CourseId = model.CourseId;
                result.Data.Name = model.Name;
                result.Data.Description = model.Description;
                result.Data.StartDate = model.StartDate;
                result.Data.Link = model.Link;
                result.Data.Queue = last + 1;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ? 
                    await _sessionService.UpdateOrDelete(result.Data) : 
                    await _sessionService.InsertAsync(result.Data);
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
        public async Task<JsonResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _sessionService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _sessionService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }

        public IActionResult GetSidebarViewComponent(int courseId)
        {
            return ViewComponent("Session", new { CourseId = courseId });
        }

        public IActionResult GetSessionViewComponent(int? id)
        {
            return ViewComponent("Session", new { Id = id });
        }
    }
}
