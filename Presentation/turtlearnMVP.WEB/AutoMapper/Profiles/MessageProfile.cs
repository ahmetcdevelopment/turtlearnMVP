using AutoMapper;
using turtlearnMVP.Domain.DTOs.ApiDTOs;
using turtlearnMVP.Domain.Entities;

namespace turtlearnMVP.WEB.AutoMapper.Profiles;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<MessageApiDTO, Message>().ReverseMap();
    }
}
