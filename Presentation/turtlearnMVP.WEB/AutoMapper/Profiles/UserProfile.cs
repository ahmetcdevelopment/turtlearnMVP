using AutoMapper;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddViewModel, User>();
            CreateMap<UserUpdateViewModel, User>().ReverseMap();
        }
    }
}
