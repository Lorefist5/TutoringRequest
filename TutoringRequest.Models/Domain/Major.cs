using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class Major : BaseEntity
{
    public string MajorName { get; set; } = default!;
    public List<Course> Courses { get; set; }
}
