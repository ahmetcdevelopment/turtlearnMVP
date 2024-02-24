using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.AutoMapper.Profiles;

public class HomeworkProfile : Profile
{
    protected HomeworkProfile()
    {
        CreateMap<HomeworkApiDTO, Homework>().ReverseMap();
    }
}
