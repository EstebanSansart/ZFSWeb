using Api.Dtos;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User,LoginDto>().ReverseMap();
        CreateMap<UserReaction,UserReactionDto>().ReverseMap();
        CreateMap<UserTag,UserTagDto>().ReverseMap();
        CreateMap<Company,CompanyDto>().ReverseMap();
        CreateMap<Event,EventDto>().ReverseMap();
        CreateMap<Event,EventoImageDto>().ReverseMap();
        CreateMap<Gender,GenderDto>().ReverseMap();
        CreateMap<Level,LevelDto>().ReverseMap();
        CreateMap<Reaction,ReactionDto>().ReverseMap();
        CreateMap<Tag,TagDto>().ReverseMap();
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<User,UserNoLevelDto>().ReverseMap();
        CreateMap<Image,ImageDto>().ReverseMap();
        CreateMap<EventAttendance,EventAttendanceDto>().ForMember(dest => dest.Cedula, opt => opt.MapFrom(src => src.UserCc)).ReverseMap();

    }
}