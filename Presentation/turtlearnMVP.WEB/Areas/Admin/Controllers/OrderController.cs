using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly UserManager<User> _userService;

    public OrderController(IOrderService orderService, UserManager<User> userService)
    {
        _orderService = orderService;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> AddOrUpdate(int? id)
    {
        var model = new OrderEditViewModel();
        model.SelStatus = EnumHelper.GetEnumSelectList<OrderStatus>();
        if (id.HasValue && id.Value > 0)
        {
            var entity = (await _orderService.GetById(id.Value)).Data ?? new Order(); // Assuming there is a GetCourseById method in _mainService
            model.Id = entity.Id;
            model.AmountPaid = entity.AmountPaid;
            model.PhoneNumber = entity.PhoneNumber;
            model.EmailConfirmCode = entity.EmailConfirmCode;
            model.Date = entity.Date;
            model.Status = entity.Status;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.FindByIdAsync(userId);
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(OrderEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _orderService.GetById(model.Id.HasValue ? model.Id.Value : 0);
            if (result.ResultStatus == ResultStatus.Success && result.Data.Id < 0)
            {
                return Json(new { Result = result });
            }
            //burayı daha sonra bağlı olan kullanıcdan aldığımız bilgiler ile dolduracağız
            result.Data.FirstName = model.FirstName;
            result.Data.LastName = model.LastName;
            result.Data.PhoneNumber = model.PhoneNumber;
            result.Data.EmailConfirmCode = model.EmailConfirmCode;
            result.Data.Email = model.Email;
            result.Data.AmountPaid = model.AmountPaid;
            result.Data.Date = model.Date;
            result.Data.Status = model.Status;
            if (result.Data.Id == 0)
            {
                result.Data.IsDeleted = false;
                result.Data.UpdateDate = DateTime.Now;
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result.Data.UpdateUserId = userId != null && int.TryParse(userId, out int _userId) ? _userId : 0;
            }
            var addOrUpdateResult = result.Data.Id > 0 ? await _orderService.UpdateOrDelete(result.Data)
                : await _orderService.InsertAsync(result.Data);
            //var component = ViewComponent("Course");
            return Json(new { Result = addOrUpdateResult/*, Compoent = component */});
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
    }
    public async Task<JsonResult> Delete(int? id)
    {
        if (id.HasValue)
        {
            var result = await _orderService.GetById(id.Value);
            if (result.ResultStatus == ResultStatus.Error)
            {
                return Json(new { Result = result });
            }
            result.Data.IsDeleted = true;
            var updateResut = await _orderService.UpdateOrDelete(result.Data);
            return Json(new { Result = updateResut });
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
    }
    public async Task<JsonResult> GetAllToGrid()
    {
        var data = _orderService.FetchAllDtos().Data;
        return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
    }
}
