﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;

        public HomeworkController(IHomeworkService homeworkService)
        {
            _homeworkService = homeworkService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeworkListViewModel();
            return View();
        }
        public async Task<JsonResult> GetAllToGrid()
        {
            var data = _homeworkService.FetchAllDtos().Data;
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(int? id)
        {
            var model = new HomeworkEditViewModel();
            if (id.HasValue && id.Value > 0)
            {
                var resultData = (await _homeworkService.GetById(id.Value)).Data ?? new Homework();
                // Assuming there is a GetCourseById method in _mainService
                model.Id = resultData.Id;
                model.SessionId = resultData.SessionId;
                model.StartDate = resultData.StartDate;
                model.EndDate = resultData.EndDate;
                model.Title = resultData.Title;
                model.UploadFile = resultData.UploadFile;
                model.Description = resultData.Description;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(HomeworkEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _homeworkService.GetById(model.Id.HasValue ? model.Id.Value : 0);
                if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
                {
                    return Json(new { Result = result });
                }
                result.Data.SessionId = model.SessionId;
                result.Data.Title = model.Title;
                result.Data.StartDate = model.StartDate;
                result.Data.EndDate = model.EndDate;
                result.Data.Description = model.Description;
                result.Data.UploadFile = model.UploadFile;
                if (result.Data.Id == 0)
                {
                    result.Data.IsDeleted = false;
                    result.Data.UpdateDate = DateTime.Now;
                    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
                }
                var addOrUpdateResult = result.Data.Id > 0 ?
                    await _homeworkService.UpdateOrDelete(result.Data) :
                    await _homeworkService.InsertAsync(result.Data);
                return Json(new { Result = addOrUpdateResult });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var result = await _homeworkService.GetById(id.Value);
                if (result.ResultStatus == ResultStatus.Error)
                {
                    return Json(new { Result = result });
                }
                result.Data.IsDeleted = true;
                var updateResut = await _homeworkService.UpdateOrDelete(result.Data);
                return Json(new { Result = updateResut });
            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
        }
    }
}
