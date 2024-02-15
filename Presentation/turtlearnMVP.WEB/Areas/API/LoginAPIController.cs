using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Persistance.Configurations;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class LoginAPIController : ControllerBase
{
    private readonly UserManager<User> _UserManager;
    private readonly SignInManager<User> _SignInManager;
    private readonly IValidator<UserLoginDto> _Validator;
    public LoginAPIController(UserManager<User> userManager, SignInManager<User> signInManager, IValidator<UserLoginDto> validator)
    {
        _UserManager = userManager;
        _SignInManager = signInManager;
        _Validator = validator;
    }
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm] UserLoginDto loginDTO)
    {
        var validationResult = await _Validator.ValidateAsync(loginDTO);
        if (!validationResult.IsValid)
        {
            // FluentValidationExtensions'dan incelenebilir. Validationları apiye gönderebilmek için.
            return BadRequest(new ApiResult(string.Empty, ResultStatus.Info, Messages.SomethingWrong, validationResult.ToDict()));
        }
        var user = await _UserManager.FindByEmailAsync(loginDTO.Email);
        if (user != null)
        {
            var result = await _SignInManager.PasswordSignInAsync(user, loginDTO.Password, loginDTO.RememberMe, false);
            if (result.Succeeded)
            {
                HttpContext.Response.Cookies.Append("UserProfilePhoto", user?.Photo);
                // sadece giriş başarılı olduğu zaman key veriyoruz.
                return Ok(new ApiResult(await turtlearnApiSetting.getKey(), ResultStatus.Success, $"Hoşgeldiniz."));
            }
            else
            {
                return BadRequest(new ApiResult("", ResultStatus.Error, $"Şifeniz yanlış, tekrar deneyiniz."));
            }
        }
        else
        {
            return BadRequest(new ApiResult("", ResultStatus.Error, $"Şifeniz yanlış, tekrar deneyiniz."));
        }
    }
    [AllowAnonymous]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _SignInManager.SignOutAsync();
        Response.Cookies.Delete("UserProfilePhoto");
        return Ok(new ApiResult("", ResultStatus.Success, $"Çıkış Başarılı!"));
    }
}
