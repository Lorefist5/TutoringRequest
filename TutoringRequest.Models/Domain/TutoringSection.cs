using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class TutoringSection : BaseEntity
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; }
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

}

