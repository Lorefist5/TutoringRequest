using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Students;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles : Profile
{
    public void ConfigureStudentMappings()
    {
        CreateMap<StudentDto, Account>().ReverseMap();
    }
}
