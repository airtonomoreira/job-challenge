using AutoMapper;
using Api.Domain.Entities;
using Api.Domain.Dtos;

namespace Api.CrossCutting.Mappins
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeEntity, EmployeeDto>();
            CreateMap<EmployeeDto, EmployeeEntity>();
            CreateMap<EmployeeDtoUpdate, EmployeeEntity>();

            CreateMap<EmployeeDtoCreate, EmployeeEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserEntity
                {
                    Email = src.Email,
                    HierarchyLevel = src.HierarchyLevel,
                    IsActive = true,
                    PasswordHash = src.Password  // This will be hashed in the service
                }));

            CreateMap<EmployeeEntity, EmployeeDtoResult>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src =>
                    src.Manager != null ? $"{src.Manager.FirstName} {src.Manager.LastName}" : null));
        }
    }
}
