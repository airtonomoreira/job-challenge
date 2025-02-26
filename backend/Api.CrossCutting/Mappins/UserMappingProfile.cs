using Api.Domain.Dtos;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappins
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            
            
            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.HierarchyLevel, opt => opt.MapFrom(src => src.HierarchyLevel));
        }
    }
}
