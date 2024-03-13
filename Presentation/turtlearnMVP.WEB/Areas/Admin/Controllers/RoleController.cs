using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.DTOs;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class RoleController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IClaimService _claimService;
    //private readonly IMapper _mapper;

    public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager, IClaimService claimService /*,IMapper mapper*/)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _claimService = claimService;
        //_mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous] //şimdilik
    public async Task<JsonResult> GetAllToGrid()
    {
        var data = await _roleManager.Roles.ToListAsync();
        return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
    }
    //[Authorize(Policy = "TeacherHomeworkAdd")]
    [AllowAnonymous] //şimdilik
    [HttpGet]
    public async Task<IActionResult> AddOrUpdate(int? id)
    {
        //IList<ClaimsByModulesDTO> modules = null;
        var model = new RoleEditViewModel();
       
        if (id.HasValue && id.Value > 0)
        {
            var role = await _roleManager.FindByIdAsync(id.Value.ToString());
            //eğer var olan bir role çağırılıyorsa modele map ediyorum.
            //model = _mapper.Map<RoleEditViewModel>(role);
            //var claims = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();
            //model.Modules[0].Claims[0].Checked = "checked";
            
            model.Id = role.Id;
            model.Name = role.Name;
            model.Modules = _claimService.FetchAllDtosByModulesWithCheckeds(role).Data;
        }
        if(id == 0)
        {
            model.Modules = _claimService.FetchAllDtosByModules().Data;
        }
        return PartialView(model);
    }
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous] //şimdilik
    [HttpPost]
    public async Task<IActionResult> AddOrUpdate(RoleEditViewModel model)
    {
        TurtLearn.Shared.Utilities.Results.Abstract.IResult jsonResult;
        if (!ModelState.IsValid)//burada bir problem var !!!!!!!
        {
            var ExistingRole = await _roleManager.FindByNameAsync(model.Name);
            ModelState.AddModelError(string.Empty, "Bu rol zaten mevcut.");
            return View("Error");
        }
        IdentityResult result;
        var role = await _roleManager.FindByIdAsync(model.Id.ToString()) ?? new Role();
        role.Name = model.Name;
        result = model.Id > 0 ? await _roleManager.UpdateAsync(role) : await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
           
            jsonResult = new Result(ResultStatus.Error, $"Kayıt işlemi sırasında bir hata meydana geldi.");
            return Json(new { Result = jsonResult });
        }

        // sadece int parse edilebilen ve null olmayan seçili id elemanları
        var modellist = model.SelectedIds.Trim('[', ']').Split(",").ToList();

        List<int> selectedIds = new List<int>();
        for (int i = 0; i <modellist.Count ; i++)
        {
            //var last = model.SelectedIds[i];
            if (int.TryParse(modellist[i], out int parsedInt) && parsedInt > 0)
            {
                selectedIds.Add(parsedInt);
            }
        }

        if (selectedIds.Count > 0)
        {
            var dbclaims = _claimService.FetchAllDtosByIds(selectedIds).Data;

            var roleClaims = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();

            var unselectedClaims = roleClaims.Where(x => !dbclaims.Any(y => y.ClaimTypeStr == x.Type && y.ClaimValue == x.Value)).ToList(); //eksikleri tamamla claim sayafasını da düzelt sonra da user

            foreach (var claim in dbclaims)
            {
                var checkClaim = roleClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
                if (checkClaim == null)
                {
                    //await _roleManager.RemoveClaimAsync(role, checkClaim);
                    var newClaim = new Claim(claim.ClaimTypeStr, claim.ClaimValue);
                    await _roleManager.AddClaimAsync(role, newClaim);
                }
                
            }

            if(unselectedClaims != null)
            {
                foreach(var claim in unselectedClaims)
                {
                    //var deleteClaim = roleClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
                    await _roleManager.RemoveClaimAsync(role, claim);
                }
            }

        }

        jsonResult = new Result(ResultStatus.Success, $"Kayıt işlemi başarıyla gerçekleştirildi.");
        return Json(new { Result = jsonResult });
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
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
