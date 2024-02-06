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
}

