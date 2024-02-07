using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    private readonly IMapper _mapper;

    public UserController
        (UserManager<User> userManager, RoleManager<Role> roleManager,
        IImageHelper imageHelper, IMapper mapper, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _imageHelper = imageHelper;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    public async Task<JsonResult> GetAllToGrid()
    {
        var data = await _userManager.Users.ToListAsync();
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
            var user = _mapper.Map<User>(model);
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
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> UpdateUser(int id)
    {
        //burada bi id kontrolü gerekiyor
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        var userUpdateDto = _mapper.Map<UserUpdateViewModel>(user);
        return PartialView(userUpdateDto);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateUser(UserUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            bool isNewPictureUploaded = false;
            var oldUser = await _userManager.FindByIdAsync(model.Id.ToString());
            var oldUserPicture = oldUser.Photo;
            if (model.PictureFile != null)
            {
                var uploadedImageResult = await _imageHelper.UploadUserImg(model.UserName, model.PictureFile); //picture alanına resim adı ekledik
                model.Photo = uploadedImageResult.ResultStatus == ResultStatus.Success ? uploadedImageResult.Data.FullName
                    : "userImages/defaultUser.png";
                if (oldUserPicture != "userImages/defaultUser.png")
                {
                    isNewPictureUploaded = true;
                }
            }
            var updatedUser = _mapper.Map<UserUpdateViewModel, User>(model, oldUser);
            var result = await _userManager.UpdateAsync(updatedUser);
            if (result.Succeeded)
            {
                if (isNewPictureUploaded)
                {
                    _imageHelper.DeleteImg(oldUserPicture);
                }
                Result _OkResult = new Result(ResultStatus.Success, $"{updatedUser.FirstName} {updatedUser.LastName}" +
                    $" isimli kullanıcı başarıyla güncellenmiştir.");
                return Json(new { Result = _OkResult });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                Result _InfoResult = new Result(ResultStatus.Info, $"Kullanıcı güncellenirken bir hata meydana geldi.");
                return Json(_InfoResult);
            }

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
}