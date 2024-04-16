using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Application.Persistance.Services;
using turtlearnMVP.Domain.Entities;
using turtlearnMVP.Domain.Enums;
using turtlearnMVP.WEB.Areas.Admin.Models;
using turtlearnMVP.WEB.Helpers;
using turtlearnMVP.WEB.Helpers.Abstract;
using turtlearnMVP.WEB.Models;
using turtlearnMVP.WEB.Models.User;

namespace turtlearnMVP.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;
        private readonly IUserSettingService _userSettingService;
        private readonly IImageHelper _imageHelper;
        public UserController
            (
                UserManager<User> userManager, 
                SignInManager<User> signInManager,
                IMailService mailService,
                IUserSettingService userSettingService,
                IImageHelper imageHelper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _userSettingService = userSettingService;
            _imageHelper = imageHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile(string? userName)
        {
           
            var model = new UserProfileViewModel();
            if(userName == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    userName = User.Identity.Name;
                }
                else
                {
                    var errorModel = new ErrorViewModel();
                    errorModel.ErrorCode = 404;
                    errorModel.ErrorDescription = "Böyle Bir Kullanıcı Mevcut Değil!";
                    errorModel.ErrorMessage = "Bulunamadı";
                    return View("Error", errorModel);
                }
            }
            var user = _userManager.FindByNameAsync(userName).GetAwaiter().GetResult();
            if(user == null)
            {
                var errorModel = new ErrorViewModel();
                errorModel.ErrorCode = 404;
                errorModel.ErrorDescription = "Böyle Bir Kullanıcı Mevcut Değil!";
                errorModel.ErrorMessage = "Bulunamadı";
                return View("Error", errorModel);
            }
            if(user.UserName == User.Identity.Name)
            {
                model.IsHimself = true;
                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
            }

            model.Id = user.Id;//değişebilir
            model.UserName = user.UserName;
            model.Picture = user.Photo;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Biography = user.Biography == null || user.Biography.Length < 1  ? "Bu kullanıcı henüz hakkında bir şeyler söylemedi. Ama eminiz harika biridir" : user.Biography ;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var model = new UserEditViewModel();
            //model.SelStatus = EnumHelper.GetEnumSelectList<CourseStatus>();
            //model.SelCategories = GetCategoriesAsSelectList();
            if (id.HasValue && id.Value > 0)
            {
                var user = (await _userManager.FindByIdAsync(id.Value.ToString())); // Assuming there is a GetCourseById method in _mainService
                model.Id = user.Id;
                model.UserName = user.UserName;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.PictureStr = user.Photo;
                model.Biography = user.Biography;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if(user == null)
                {
                    return Json(new { Result = "Kullanıcı bulunamadı!" });
                }

                if(user.Email != model.Email)
                {
                    var userSetting = await _userSettingService.GetByUserIdAndKey(user.Id, (int)UserSettingType.Verify, (int)UserSettingVerify.Email);
                    if(userSetting.ResultStatus == ResultStatus.Success)
                    {
                        userSetting.Data.IsDeleted = true;
                        _userSettingService.UpdateOrDelete(userSetting.Data);
                    }
                }

                user.UserName = model.UserName;
                user.FirstName  = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Biography = model.Biography;
                
                if (model.Picture != null)
                {
                   user.Photo = _imageHelper.UploadUserImg(user.UserName, model.Picture, "avatar").GetAwaiter().GetResult().Data.FileName;
                }
                var addOrUpdateResult =  await _userManager.UpdateAsync(user);
                ////var component = ViewComponent("Course");
                return Json(new { Result = addOrUpdateResult/*, Compoent = component */});

            }
            return Json(new { Result = new Result(ResultStatus.Error, Messages.PageIsNotFound) });
        }

        [HttpGet]
        public async Task<IActionResult> LoginOrRegisterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            UserLoginDto model = new UserLoginDto();
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null /*&& user.IsDeleted == false*/)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Response.Cookies.Append("UserProfilePhoto", user.Photo);
                        return Json(new { success = true, message = "Giriş Başarılı!", redirectUrl = Url.Action("Index", "Home") });
                    }
                    else
                    {
                        return Json(new { success = false, message = "E-Posta adresiniz veya şifreniz yanlış." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "E-Posta adresiniz veya şifreniz yanlış." });
                }
            }
            else
            {
                var validationErrors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage);

                return Json(new { success = false, message = "Validation failed", errors = validationErrors });
                //return Json(new { success = false, message = "Formu eksiksiz doldurunuz." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            UserRegisterViewModel model = new UserRegisterViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            //model.Email = HttpContext.Session.GetString("VerifiedMail").Length > 0 ? HttpContext.Session.GetString("VerifiedMail") : null;

            if (ModelState.IsValid)
            {
                var verifiedMail = HttpContext.Session.GetString("VerifiedMail");

                if (model.Email != verifiedMail)
                {
                    return Json(new { success = false, message = "Doğrulama Başarısız! Mail Doğrulamasının ardından tekrar deneyiniz!" });
                }

                if (model.Password != model.PasswordCheck)
                {
                    return Json(new { success = false, message = "Şifreleriniz Uyuşmuyor! Kontrol edip tekrar deneyiniz!"});
                }
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Photo = "default.jpg",
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                    
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userSetting = new UserSetting
                    {
                        UserId = user.Id,
                        TypeId = (int)UserSettingType.Verify,
                        Key = (int)UserSettingVerify.Email,
                        Value = model.Email
                    };
                    _userSettingService.InsertAsync(userSetting).GetAwaiter().GetResult();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Response.Cookies.Append("UserProfilePhoto", user.Photo);
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return Json(new { success = false, message = "Kaydolurken bir hata meydana geldi!", errors = errors });
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Formu eksiksiz doldurup tekrar deneyiniz", errors = errors });
            }
        }

        [Authorize]
        //[HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("UserProfilePhoto");
            return Json(new { success = true, message = "Çıkış Başarılı!", redirectUrl = Url.Action("Index", "Home") });
        }

        public async Task<IActionResult> Verify(string targetMail)
        {
            var code = _userSettingService.GenerateRandomVerificationCode().GetAwaiter().GetResult();
            var result = _mailService.SendVerificationCodeAsync(targetMail, code).GetAwaiter().GetResult();
            if(result.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetInt32("VerifyCode", code);
                HttpContext.Session.SetString("VerifyTargetMail", targetMail);
                return Json(new { success = true});
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> VerifyCode(int code)
        {
            var verifyCode = HttpContext.Session.GetInt32("VerifyCode");
            if (verifyCode == code)
            {
                var verifiedMail = HttpContext.Session.GetString("VerifyTargetMail");
                HttpContext.Session.SetString("VerifiedMail", verifiedMail);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult GetSliderViewComponent(int? teacherId)
        {
            return ViewComponent("UserCoursesSlider", new { TeacherId = teacherId });
        }

    }
}
