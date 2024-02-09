using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Courses;

public class AddCourseDto
{
    public string CourseName { get; set; } = default!;
    public Guid MajorId { get; set; }
}
