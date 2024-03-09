using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.AutoMapper.Profiles
{
    public class HomeworkTransferProfile : Profile
    {
        protected HomeworkTransferProfile()
        {
            CreateMap<HomeworkTransferApiDTO, Homework>().ReverseMap();
        }
    }
}
