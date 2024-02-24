using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;
namespace turtlearnMVP.WEB.AutoMapper.Profiles;
public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<SessionApiDTO, Session>().ReverseMap();
    }
}
