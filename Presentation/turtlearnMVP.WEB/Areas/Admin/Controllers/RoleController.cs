using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class RoleController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    public async Task<JsonResult> GetAllToGrid()
    {
        var data = await _roleManager.Roles.ToListAsync();
        return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> AddOrUpdateRole(int? id)
    {
        var model = new RoleEditViewModel();
        if (id.HasValue)
        {
            var role = await _roleManager.FindByIdAsync(id.Value.ToString());
            //eğer var olan bir role çağırılıyorsa modele map ediyorum.
            model = _mapper.Map<RoleEditViewModel>(role);
        }
        return PartialView(model);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddOrUpdateRole(RoleEditViewModel model)
    {
        TurtLearn.Shared.Utilities.Results.Abstract.IResult jsonResult;
        if (ModelState.IsValid)
        {
            var ExistingRole = await _roleManager.FindByNameAsync(model.Name);
            ModelState.AddModelError(string.Empty, "Bu rol zaten mevcut.");
            return View("Error");
        }
        IdentityResult result;
        var role = await _roleManager.FindByIdAsync(model.Id.ToString()) ?? new Role();
        role.Name = model.Name;
        result = model.Id > 0 ? await _roleManager.UpdateAsync(role) : await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
            jsonResult = new Result(ResultStatus.Success, $"Kayıt işlemi başarıyla gerçekleştirildi.");
            return Json(new { Result = jsonResult });
        }
        jsonResult = new Result(ResultStatus.Error, $"Kayıt işlemi sırasında bir hata meydana geldi.");
        return Json(new { Result = jsonResult });
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role == null || role.Id > 0)
        {
            return Json(new
            {
                Result = new Result(ResultStatus.Error, $"Silmeye çalıştığınız rol bulunamadı.")
            });
        }
        var deleteResult = await _roleManager.DeleteAsync(role);
        return deleteResult.Succeeded ?
            Json(new { Result = new Result(ResultStatus.Success, $"{role.Name} adlı rol başarıyla silinmiştir.") }) :
            Json(new { Result = new Result(ResultStatus.Error, $"{role.Name} adlı rol silinirken bi hata meydana geldi") });
    }
}
