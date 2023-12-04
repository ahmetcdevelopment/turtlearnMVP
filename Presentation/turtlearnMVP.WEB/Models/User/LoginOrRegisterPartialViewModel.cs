using TurtLearn.Shared.Entities.Dtos;

namespace turtlearnMVP.WEB.Models.User
{
    public class LoginOrRegisterPartialViewModel
    {
        public UserLoginDto LoginModel { get; set; }
        public UserDto RegisterModel { get; set; }
    }
}
