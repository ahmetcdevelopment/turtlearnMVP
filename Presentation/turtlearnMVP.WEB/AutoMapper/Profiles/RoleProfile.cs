using AutoMapper;
using TurtLearn.Shared.Entities.Concrete;
using turtlearnMVP.WEB.Areas.Admin.Models;

namespace turtlearnMVP.WEB.AutoMapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEditViewModel, Role>().ReverseMap();
        }
    }
}
