using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class TutoringSection : BaseEntity
{
    public Guid StudentId { get; set; }
    public Account Student { get; set; }
    public Guid TutorId { get; set; }
    public Account Tutor { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}

