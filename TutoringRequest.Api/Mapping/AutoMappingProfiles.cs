using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Mapping;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<TutorDto, Tutor>().ReverseMap();
        CreateMap<AddTutorRequest, Tutor>().ReverseMap();
        CreateMap<AvailabilitySlot, AvailabilityDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));
        CreateMap<AddAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
        CreateMap<UpdateAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
    }
    public static IMapper Configure()
    {
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TutorDto, Tutor>().ReverseMap();
            cfg.CreateMap<AddTutorRequest, Tutor>().ReverseMap();
            cfg.CreateMap<AvailabilitySlot, AvailabilityDto>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));
            cfg.CreateMap<AddAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
            cfg.CreateMap<UpdateAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
        });

        IMapper mapper = mapperConfiguration.CreateMapper();
        return mapper;
    }
}

