using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TurtLearn.Shared.Entities.Concrete;
using TurtLearn.Shared.Utilities.Messages;
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
            if (string.IsNullOrEmpty(key) || await turtlearnApiSetting.isKeyValid(key))
            {
                return BadRequest(Messages.PageIsNotFound);
            }
            //Devamını yaz.
            return BadRequest(Messages.PageIsNotFound);

        }
    }
}
