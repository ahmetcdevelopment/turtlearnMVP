using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers.Abstract;
using turtlearnMVP.WEB.Helpers.Extensions;

namespace turtlearnMVP.WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IImageHelper _imageHelper;
    private readonly IClaimService _claimService;
    private readonly IZrfRoleService _zrfRoleService;
    //private readonly IMapper _mapper;

    public UserController
        (UserManager<User> userManager, RoleManager<Role> roleManager,
        IImageHelper imageHelper, /*IMapper mapper,*/ SignInManager<User> signInManager, IClaimService claimService, IZrfRoleService zrfRoleService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _imageHelper = imageHelper;
        //_mapper = mapper;
        _signInManager = signInManager;
        _claimService = claimService;
        _zrfRoleService = zrfRoleService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    //[Authorize(Roles = "Admin")] şimdilik
    public async Task<JsonResult> GetAllToGrid()
    {
        var data = await _userManager.Users.ToListAsync();
        //foreach (var user in data) //Kullanıcı tipi ayarlandıktan ve user güncellendikten sonra geri dönülüp düzeltilcek. kayıt olma kurgusununun ardından düzelecek.
        //{
        //    string userTypeStr = user.UserTypeStr;  // Assuming this property exists
        //    DateTime registerDate = user.RegisterDate; // Assuming this property exists

        //    // Utilize these values as needed, e.g., for logging, additional processing, etc.
        //}
        return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> AddUser()
    {
        return PartialView();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddUser(UserAddViewModel model)
    {
        if (ModelState.IsValid)
        {
            var uploadedImageResult = await _imageHelper.UploadUserImg(model.UserName, model.PictureFile); //picture alanına resim adı ekledik
            model.Photo = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName
                : "userImages/defaultUser.png";
            //var user = _mapper.Map<User>(model);
            var user = new User();
            //user özellikleri
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Result _OkResult = new Result(ResultStatus.Success, $"{user.FirstName} {user.LastName} isimli" +
                    $"kullanıcı başarıyla eklenmiştir. ");
                return Json(new { Result = _OkResult });
            }
            else
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                Result _InfoResult = new Result(ResultStatus.Info, $"Kullanıcı eklenirken bir hata meydana geldi.");
                return Json(new { Result = _InfoResult });
            }
        }
        Result _BadResult = new Result(ResultStatus.Error, $"Kullanıcı eklenirken bir hata meydana geldi");
        return Json(new { Result = _BadResult });
    }
    [Authorize(Roles = "Admin")]
    public async Task<JsonResult> Delete(int? id)
    {
        if (id.HasValue)
        {
            var user = await _userManager.FindByIdAsync(id.Value.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new
                {
                    Result = new Result(ResultStatus.Success, $"{user.FirstName} " +
                    $"{user.LastName} isimli kullanıcı başarıyla silinmiştir.")
                });
            }
            //العدل أساس الملكية
            return Json(new { Result = new Result(ResultStatus.Error, $"Kullanıcı silinirken bir hata oluştu.") });
        }
        return Json(new { Result = new Result(ResultStatus.Error, Messages.ResultIsNotFound) });
    }
    //[Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> UpdateUserAuth(int id)
    {

        //var userUpdateDto = _mapper.Map<UserUpdateViewModel>(user);
        var model = new UserAuthUpdateViewModel();

        if (id != null && id > 0)
        {//burada bi id kontrolü gerekiyor
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            //eğer var olan bir role çağırılıyorsa modele map ediyorum.
            //model = _mapper.Map<RoleEditViewModel>(role);
            //var claims = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();
            //model.Modules[0].Claims[0].Checked = "checked"; //burayı düşüneceğim 

            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.Photo = user.Photo;
            //model.UserType = "Kullanıcı Türü";
            //model.RegisterDate = user.
            var type = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            model.UserType = type.FirstOrDefault();
            model.Modules = _claimService.FetchAllDtosByModulesWithCheckeds(user).Data;
            model.Roles = _zrfRoleService.FetchAllDtosWithCheckeds(user).Data;
        }
        if (id == 0)
        {
            model.Modules = _claimService.FetchAllDtosByModules().Data;
            model.Roles = _zrfRoleService.FetchAllDtos().Data;
        }
        return PartialView(model);
    }
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateUserAuth(UserAuthUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = "Yetkilendirmede bir değişiklik olmadı!";
            var user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);
            //bool isNewPictureUploaded = false;
            //var oldUser = await _userManager.FindByIdAsync(model.Id.ToString());
            //var oldUserPicture = oldUser.Photo;
            //if (model.PictureFile != null)
            //{
            //    var uploadedImageResult = await _imageHelper.UploadUserImg(model.UserName, model.PictureFile); //picture alanına resim adı ekledik
            //    model.Photo = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName
            //        : "userImages/defaultUser.png";
            //    if (oldUserPicture != "userImages/defaultUser.png")
            //    {
            //        isNewPictureUploaded = true;
            //    }
            //}
            //var updatedUser = _mapper.Map<UserUpdateViewModel, User>(model, oldUser);
            //var updatedUser = new User();
            //updatedUser.Id = model.Id;
            //updatedUser.UserName = model.UserName; ////sonra değişir


            #region UserClaim
            // sadece int parse edilebilen ve null olmayan seçili id elemanları
            var modelClaimList = model.SelectedIds.Trim('[', ']').Split(",").ToList();

            List<int> selectedClaimIds = new List<int>();
            for (int i = 0; i < modelClaimList.Count; i++)
            {
                //var last = model.SelectedIds[i];
                if (int.TryParse(modelClaimList[i], out int parsedInt) && parsedInt > 0)
                {
                    selectedClaimIds.Add(parsedInt);
                }
            }

            if (selectedClaimIds.Count > 0)
            {
                var dbclaims = _claimService.FetchAllDtosByIds(selectedClaimIds).Data;

                var userClaims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

                var unselectedClaims = userClaims.Where(x => !dbclaims.Any(y => y.ClaimTypeStr == x.Type && y.ClaimValue == x.Value)).ToList(); //eksikleri tamamla claim sayafasını da düzelt sonra da user

                foreach (var claim in dbclaims)
                {
                    var checkClaim = userClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
                    if (checkClaim == null)
                    {
                        //await _roleManager.RemoveClaimAsync(role, checkClaim);
                        var newClaim = new Claim(claim.ClaimTypeStr, claim.ClaimValue);
                        await _userManager.AddClaimAsync(user, newClaim);
                    }

                }

                if (unselectedClaims != null)
                {
                    foreach (var claim in unselectedClaims)
                    {
                        //var deleteClaim = roleClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
                        await _userManager.RemoveClaimAsync(user, claim);
                    }
                }

            }

            #endregion


            #region UserRole
            // sadece int parse edilebilen ve null olmayan seçili id elemanları
            var modelRoleList = model.SelectedRoleIds.Trim('[', ']').Split(",").ToList();
            //var user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);

            List<int> selectedRoleIds = new List<int>();
            for (int i = 0; i < modelRoleList.Count; i++)
            {
                //var last = model.SelectedIds[i];
                if (int.TryParse(modelRoleList[i], out int parsedInt) && parsedInt > 0)
                {
                    selectedRoleIds.Add(parsedInt);
                }
            }

            if (selectedRoleIds.Count > 0)
            {
                var selectedRoles = _zrfRoleService.FetchAllDtosByIds(selectedRoleIds).Data;

                var userRoles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

                var rolesForAdd = new List<string>();
                var rolesForDelete = new List<string>();

                var unselectedRoles = userRoles.Where(x => !selectedRoles.Any(y => y.Name == x)).ToList(); //burada bir problem var kurguyu iyi düşün
                foreach (var role in selectedRoles)
                {
                    var checkRole = userRoles.Where(x => x == role.Name).FirstOrDefault();
                    if (checkRole == null)
                    {
                        //await _roleManager.RemoveClaimAsync(role, checkClaim);
                        //var newClaim = new Claim(claim.ClaimTypeStr, claim.ClaimValue);
                        rolesForAdd.Add(role.Name);
                    }

                }

                if (unselectedRoles != null)
                {
                    foreach (var role in unselectedRoles)
                    {
                        //var deleteClaim = roleClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
                        //await _userManager.RemoveClaimAsync(user, claim);
                        rolesForDelete.Add(role);
                    }
                }
                
                if (rolesForAdd != null && rolesForAdd.Count > 0)
                    result = _userManager.AddToRolesAsync(user, rolesForAdd.AsEnumerable()).GetAwaiter().GetResult().Succeeded ? "Yeni Roller Başarıyla Atandı!" : "Yeni roller atanırken bir sorun oluştu!" ;

                if (rolesForDelete != null && rolesForDelete.Count > 0)
                    result = _userManager.RemoveFromRolesAsync(user, rolesForDelete.AsEnumerable()).GetAwaiter().GetResult().Succeeded? "Kullanıcı ilgili rollerden başarıyla temizlendi" : "Rol temizleme sırasında bir hata oluştu";

                return Json(result);

            }
            #endregion
            //var result = await _userManager.UpdateAsync(updatedUser);
            //if (result.Succeeded)
            //{
            //    if (isNewPictureUploaded)
            //    {
            //        _imageHelper.DeleteImg(oldUserPicture);
            //    }
            //    Result _OkResult = new Result(ResultStatus.Success, $"{updatedUser.FirstName} {updatedUser.LastName}" +
            //        $" isimli kullanıcı başarıyla güncellenmiştir.");
            //    return Json(new { Result = _OkResult });
            //}
            //else
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError("", error.Description);
            //    }
            //    Result _InfoResult = new Result(ResultStatus.Info, $"Kullanıcı güncellenirken bir hata meydana geldi.");
            return Json("_InfoResult");
            //}

        }
        else
        {
            Result _BadResult = new Result(ResultStatus.Error, $"Kullanıcı güncellenemedi");
            return Json(_BadResult);
        }
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> PasswordChange()
    {
        return View();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isVerify = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
            if (isVerify)
            {
                var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                    TempData.Add("SuccessMessage", $"Şifreniz başarıyla güncellenmiştir.");
                    return View();
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(userPasswordChangeDto);
                }
            }
            else//isVerify olmazsa
            {
                ModelState.AddModelError("", "Lütfen girmiş olduğunuz şuanki şifrenizi kontrol ediniz.");
                return View(userPasswordChangeDto);
            }
        }
        else
        {
            return View(userPasswordChangeDto);
        }
    }
    //[Authorize(Roles = "Admin")]
    //public Task<IActionResult> AddOrUpdateUserRoles(int userId)
    //{

    //}

    #region User Update
    //[HttpPost]
    //public async Task<IActionResult> UpdateUserAuth(UserAuthUpdateViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        bool isNewPictureUploaded = false;
    //        var oldUser = await _userManager.FindByIdAsync(model.Id.ToString());
    //        var oldUserPicture = oldUser.Photo;
    //        if (model.PictureFile != null)
    //        {
    //            var uploadedImageResult = await _imageHelper.UploadUserImg(model.UserName, model.PictureFile); //picture alanına resim adı ekledik
    //            model.Photo = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName
    //                : "userImages/defaultUser.png";
    //            if (oldUserPicture != "userImages/defaultUser.png")
    //            {
    //                isNewPictureUploaded = true;
    //            }
    //        }
    //        //var updatedUser = _mapper.Map<UserUpdateViewModel, User>(model, oldUser);
    //        var updatedUser = new User();
    //        updatedUser.Id = model.Id;
    //        updatedUser.UserName = model.UserName; //sonra değişir


    //        #region UserClaim
    //        // sadece int parse edilebilen ve null olmayan seçili id elemanları
    //        var modellist = model.SelectedIds.Trim('[', ']').Split(",").ToList();
    //        var user = _userManager.Users.FirstOrDefault(u => u.Id == model.Id);

    //        List<int> selectedIds = new List<int>();
    //        for (int i = 0; i < modellist.Count; i++)
    //        {
    //            //var last = model.SelectedIds[i];
    //            if (int.TryParse(modellist[i], out int parsedInt) && parsedInt > 0)
    //            {
    //                selectedIds.Add(parsedInt);
    //            }
    //        }

    //        if (selectedIds.Count > 0)
    //        {
    //            var dbclaims = _claimService.FetchAllDtosByIds(selectedIds).Data;

    //            var userClaims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

    //            var unselectedClaims = userClaims.Where(x => !dbclaims.Any(y => y.ClaimTypeStr == x.Type && y.ClaimValue == x.Value)).ToList(); //eksikleri tamamla claim sayafasını da düzelt sonra da user

    //            foreach (var claim in dbclaims)
    //            {
    //                var checkClaim = userClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
    //                if (checkClaim == null)
    //                {
    //                    //await _roleManager.RemoveClaimAsync(role, checkClaim);
    //                    var newClaim = new Claim(claim.ClaimTypeStr, claim.ClaimValue);
    //                    await _userManager.AddClaimAsync(user, newClaim);
    //                }

    //            }

    //            if (unselectedClaims != null)
    //            {
    //                foreach (var claim in unselectedClaims)
    //                {
    //                    //var deleteClaim = roleClaims.Where(x => x.Type == claim.ClaimTypeStr && x.Value == claim.ClaimValue).FirstOrDefault();
    //                    await _userManager.RemoveClaimAsync(user, claim);
    //                }
    //            }

    //        }

    //        #endregion

    //        var result = await _userManager.UpdateAsync(updatedUser);
    //        if (result.Succeeded)
    //        {
    //            if (isNewPictureUploaded)
    //            {
    //                _imageHelper.DeleteImg(oldUserPicture);
    //            }
    //            Result _OkResult = new Result(ResultStatus.Success, $"{updatedUser.FirstName} {updatedUser.LastName}" +
    //                $" isimli kullanıcı başarıyla güncellenmiştir.");
    //            return Json(new { Result = _OkResult });
    //        }
    //        else
    //        {
    //            foreach (var error in result.Errors)
    //            {
    //                ModelState.AddModelError("", error.Description);
    //            }
    //            Result _InfoResult = new Result(ResultStatus.Info, $"Kullanıcı güncellenirken bir hata meydana geldi.");
    //            return Json(_InfoResult);
    //        }

    //    }
    //    else
    //    {
    //        Result _BadResult = new Result(ResultStatus.Error, $"Kullanıcı güncellenemedi");
    //        return Json(_BadResult);
    //    }
    //}
    #endregion
}