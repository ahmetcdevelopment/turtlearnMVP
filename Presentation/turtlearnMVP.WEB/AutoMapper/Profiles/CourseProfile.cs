using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.AutoMapper.Profiles
{
    public class CourseProfile : Profile
    {
        protected CourseProfile()
        {
            CreateMap<CourseApiDTO, Course>().ReverseMap();
        }
    }
}
