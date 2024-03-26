using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles : Profile
{
    public void ConfigureMajorMappings()
    {
        CreateMap<AddMajorDto, Major>().ReverseMap();
        CreateMap<MajorDto, Major>().ReverseMap();
    }
}
