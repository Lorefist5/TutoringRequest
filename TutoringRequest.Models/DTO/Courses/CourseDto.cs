using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.Models.DTO.Courses;

public class CourseDto
{
    public Guid Id { get; set; }
    public string CourseName { get; set; } = default!;
    public Guid MajorId { get; set; }
}
