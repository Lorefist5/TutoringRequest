using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles : Profile
{
    public void ConfigreAvailabilitySlotMappings()
    {
        CreateMap<AvailabilitySlot, AvailabilityDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ToString()));
        CreateMap<AddAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
        CreateMap<UpdateAvailabilitySlotRequest, AvailabilitySlot>().ReverseMap();
    }
}
