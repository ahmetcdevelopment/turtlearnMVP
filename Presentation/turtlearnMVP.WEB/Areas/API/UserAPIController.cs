using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Entities.Dtos;
using TurtLearn.Shared.Utilities.Extensions;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Persistance.Configurations;

namespace turtlearnMVP.WEB.Areas.API;

[Route("turtlearnApi/[controller]")]
[ApiController]
public class UserAPIController : ControllerBase
{
    private readonly UserManager<User> _UserManager;
    private readonly RoleManager<Role> _RoleManager;
    private readonly SignInManager<User> _SignInManager;
    private readonly IMapper _Mapper;

    public UserAPIController(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, SignInManager<User> signInManager)
    {
        _UserManager = userManager;
        _RoleManager = roleManager;
        _Mapper = mapper;
        _SignInManager = signInManager;
    }
    [AllowAnonymous]
    [HttpPost("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromForm] string key)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var users = _UserManager.Users.ToList();
        var result = users != null && users.Any() ? new ApiDataResult<List<User>>(_Key, ResultStatus.Success, users) :
            new ApiDataResult<List<User>>(_Key, ResultStatus.Error, Messages.ResultIsNotFound, new List<User>());
        return result.ResultStatus == ResultStatus.Success ? Ok(result) : BadRequest(result);
    }
    [AllowAnonymous]
    [HttpPost("AddOrUpdateUser")]
    public async Task<IActionResult> AddOrUpdateUser([FromForm] string key, [FromForm] UserDto UserAddDto)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var mappedUser = _Mapper.Map<User>(UserAddDto);
        //Eğer Id'si 0 ise bu bir ekleme işlemidir, değilse bu bir güncelleme işlemidir.
        var result = mappedUser.Id == 0 ? await _UserManager.CreateAsync(mappedUser) : await _UserManager.UpdateAsync(mappedUser);
        return result.Succeeded ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessUpdate(TableExtensions.GetTableTitle<User>())))
            : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedUpdate(TableExtensions.GetTableTitle<User>())));
    }
    [AllowAnonymous]
    [HttpPost("DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromForm] string key, [FromForm] int Id)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var user = await _UserManager.FindByIdAsync(Id.ToString());
        //User entity'sine IsDeleted propertysi eklenip sadece o güncellenecek.
        var result = user != null && user.Id <= 0 ? await _UserManager.DeleteAsync(user) : new IdentityResult();
        return result.Succeeded ? Ok(new ApiResult(_Key, ResultStatus.Success, Messages.SuccessDelete(TableExtensions.GetTableTitle<User>())))
            : BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.FailedDelete(TableExtensions.GetTableTitle<User>())));
    }
    [AllowAnonymous]
    [HttpPost("RoleAddToUser")]
    public async Task<IActionResult> RoleAddToUser([FromForm] string key, [FromForm] int UserId, [FromForm] int RoleId)
    {
        //Burası ileride parametre olarak RoleId yerine IList<string> Roles alacak ve kullanıcıya 
        //tek seferde birden fazla rol ekleme veya silme işlemi yapılabilecek.
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var user = await _UserManager.FindByIdAsync(UserId.ToString());
        var role = await _RoleManager.FindByIdAsync(RoleId.ToString());
        if (user == null || user.Id <= 0 || role == null || role.Id <= 0)
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.ResultIsNotFound));
        }
        var userRoles = (await _UserManager.GetRolesAsync(user)).ToList();
        if (userRoles.Any(x => x.Trim().Equals(role.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Info, $"{user.FirstName} {user.LastName} isimli kullanıcı zaten {role.Name} rolüne sahip."));
        }
        var result = await _UserManager.AddToRoleAsync(user, role.Name);
        return result.Succeeded ? Ok(new ApiResult(_Key, ResultStatus.Success,
            $"{user.FirstName} {user.LastName} isimli kullanıcıya {role.Name} isimli rol eklenmmiştir."))
            : BadRequest(new ApiResult(_Key, ResultStatus.Error, $"Kullanıcıya rol ekleme işlemi sırasında bir hata meydana geldi."));
    }
    [AllowAnonymous]
    [HttpPost("RoleDeleteToUser")]
    public async Task<IActionResult> RoleDeleteToUser([FromForm] string key, [FromForm] int UserId, [FromForm] int RoleId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var user = await _UserManager.FindByIdAsync(UserId.ToString());
        var role = await _RoleManager.FindByIdAsync(RoleId.ToString());
        if (user == null || user.Id <= 0 || role == null || role.Id <= 0)
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.ResultIsNotFound));
        }
        var userRoles = (await _UserManager.GetRolesAsync(user)).ToList();
        if (!userRoles.Any(x => x.Trim().Equals(role.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Info, $"{user.FirstName} {user.LastName} isimli kullanıcının {role.Name} isimli rolü yok."));
        }
        var result = await _UserManager.RemoveFromRoleAsync(user, role.Name);
        return result.Succeeded ? Ok(new ApiResult(_Key, ResultStatus.Success,
            $"{user.FirstName} {user.LastName} isimli kullanıcıya {role.Name} isimli rol eklenmmiştir."))
            : BadRequest(new ApiResult(_Key, ResultStatus.Error, $"Kullanıcıya rol ekleme işlemi sırasında bir hata meydana geldi."));
    }
    [AllowAnonymous]
    [HttpPost("PasswordChange")]
    public async Task<IActionResult> PasswordChange([FromForm] string key, [FromForm] UserPasswordChangeDto userPasswordChangeDto)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        // buraya bakılacak tekrar.
        var user = await _UserManager.GetUserAsync(HttpContext.User);
        var isVerify = await _UserManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
        if (isVerify)
        {
            var result = await _UserManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
            if (result.Succeeded)
            {
                await _UserManager.UpdateSecurityStampAsync(user);
                await _SignInManager.SignOutAsync();
                await _SignInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                return Ok(new ApiResult(_Key, ResultStatus.Success, $"Şifreniz başarıyla güncellenmiştir."));
            }
            else
            {
                return BadRequest(new ApiResult(_Key, ResultStatus.Error, $"Şifreniz güncellenirken bir hata oluştu."));
            }
        }
        else//isVerify olmazsa
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, $"Şifreniz güncellenirken bir hata oluştu."));
    }
    [AllowAnonymous]
    [HttpPost("GetConnectedUserProfile")]
    public async Task<IActionResult> GetConnectedUserProfile([FromForm] string key, [FromForm] int UserId)
    {
        var _Key = await turtlearnApiSetting.getKey();
        if (string.IsNullOrEmpty(key) || !(await turtlearnApiSetting.isKeyValid(key)))
        {
            return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
        }
        var user = await _UserManager.FindByIdAsync(UserId.ToString());
        if (user == null) { return BadRequest(new ApiResult(_Key, ResultStatus.Error, $"Böyle bir kullanıcı bulunmuyor.")); }
        var mappedUser = _Mapper.Map<UserProfileApiDTO>(user);
        return mappedUser == null && mappedUser.Id <= 0 ?
            BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound)) :
            Ok(new ApiDataResult<UserProfileApiDTO>(_Key, ResultStatus.Success, mappedUser));
    }
}
