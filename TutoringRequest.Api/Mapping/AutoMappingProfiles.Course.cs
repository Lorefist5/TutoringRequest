using AutoMapper;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Courses;

namespace TutoringRequest.Api.Mapping;

public partial class AutoMappingProfiles : Profile
{
    public void ConfigureCourseMappings()
    {
        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<AddCourseDto, Course>().ReverseMap();
    }

}
