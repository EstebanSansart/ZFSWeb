using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Company,CompanyDto>().ReverseMap();
        CreateMap<EventAttendance,EventAttendanceDto>().ReverseMap();
        CreateMap<Event,EventDto>().ReverseMap();
        CreateMap<Gender,GenderDto>().ReverseMap();
        CreateMap<Level,LevelDto>().ReverseMap();
        CreateMap<Reaction,ReactionDto>().ReverseMap();
        CreateMap<Tag,TagDto>().ReverseMap();
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<UserReaction,UserReactionDto>().ReverseMap();
        CreateMap<UserTag,UserTagDto>().ReverseMap();
    }
}