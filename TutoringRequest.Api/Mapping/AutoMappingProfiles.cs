using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Admin;
using TutoringRequest.Models.DTO.AvailabilitySlot;
using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;
using TutoringRequest.Models.DTO.Roles;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        ConfigureTutorMappings();
        ConfigreAvailabilitySlotMappings();
        ConfigureMajorMappings();
        ConfigureCourseMappings();
        ConfigureRoleMappings();
    }
    public static IMapper Configure()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AvailabilitySlot, AvailabilityDto>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));
            cfg.CreateMap<AddAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
            cfg.CreateMap<UpdateAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
        });

        IMapper mapper = mapperConfiguration.CreateMapper();
        return mapper;
    }
}

