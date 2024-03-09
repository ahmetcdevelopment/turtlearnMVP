using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.AutoMapper.Profiles;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<ChatApiDTO, Chat>().ReverseMap();
    }
}
