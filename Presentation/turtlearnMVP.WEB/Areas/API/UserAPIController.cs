using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;
using TurtLearn.Shared.Utilities.Results.Concrete;
using turtlearnMVP.Persistance.Configurations;

namespace turtlearnMVP.WEB.Areas.API
{
    [Route("turtlearnApi/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public UserAPIController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromForm] string key)
        {
            var _Key = await turtlearnApiSetting.getKey();
            if (string.IsNullOrEmpty(key) || await turtlearnApiSetting.isKeyValid(key))
            {
                return BadRequest(new ApiResult(_Key, ResultStatus.Error, Messages.PageIsNotFound));
            }
            var users = _UserManager.Users.ToList();
            var result = users != null && users.Any() ? new ApiDataResult<List<User>>(_Key, ResultStatus.Success, users) :
                new ApiDataResult<List<User>>(_Key, ResultStatus.Error, Messages.ResultIsNotFound, new List<User>());
            return result.ResultStatus == ResultStatus.Success ? Ok(result) : BadRequest(result);
        }
    }
}
