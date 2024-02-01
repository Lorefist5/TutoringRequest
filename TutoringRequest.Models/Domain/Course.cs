using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class Course : BaseEntity
{
    public string CourseName { get; set; } = default!;
    public Guid MajorId { get; set; }
    public Major Major { get; set; } = default!;
}
