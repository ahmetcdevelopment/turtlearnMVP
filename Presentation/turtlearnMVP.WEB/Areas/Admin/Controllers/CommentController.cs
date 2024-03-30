using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<JsonResult> Send(CommentSendEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _commentService.GetById(model.Id.HasValue ? model.Id.Value : 0);
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
            {
                return Json(new { Result = result });
            }
            result.Data.Text = model.Text;
            result.Data.RecordId = model.RecordId;
            result.Data.TableId = TableExtensions.GetTableId<Session>();
            result.Data.ParentId = model.ParentId;
            result.Data.Rating = 5;//Şimdilik default 5 atandı ama değişmesi gerek.
            result.Data.StudentId = userId != null && int.TryParse(userId, out int _userid) ? _userid : 0;
            if (result.Data.Id == 0)
            {
                result.Data.IsDeleted = false;
                result.Data.UpdateDate = DateTime.Now;
                result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
            }
            var addOrUpdateResult = result.Data.Id > 0 ? await _commentService.UpdateOrDelete(result.Data)
                : await _commentService.InsertAsync(result.Data);
            //var component = ViewComponent("Course");
            return Json(new { Result = addOrUpdateResult/*, Compoent = component */});
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });

    }
}
