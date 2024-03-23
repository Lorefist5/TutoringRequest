using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.WebUi.ViewModel;

public class MajorViewModel
{
    public MajorViewModel(MajorDto major, IEnumerable<CourseDto> courses)
    {
        Major = major;
        Courses = courses;
    }
    public MajorDto Major { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; }
}
