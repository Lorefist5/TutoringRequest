using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.WebUi.ViewModel;

public class AddCourseViewModel
{
    public AddCourseDto AddCourseDto { get; set; }
    public IEnumerable<MajorDto> Majors { get; set; }
}
