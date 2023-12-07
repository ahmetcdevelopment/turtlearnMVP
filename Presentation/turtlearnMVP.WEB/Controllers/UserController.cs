using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Entities.Abstract;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using turtlearnMVP.WEB.Models.User;

namespace turtlearnMVP.WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController
            (
                UserManager<User> userManager, 
                SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

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
                return Json(new { success = false, message = "Formu eksiksiz doldurunuz." });
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
            if (ModelState.IsValid)
            {
                if (model.Password != model.PasswordCheck)
                {
                    return Json(new { success = false, message = "Şifreleriniz Uyuşmuyor! Kontrol edip tekrar deneyiniz!"});
                }
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
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
    }
}
