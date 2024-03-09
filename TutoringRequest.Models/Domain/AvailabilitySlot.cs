using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class AvailabilitySlot : BaseEntity
{
    public Guid TutorId { get; set; }
    public Account Tutor { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}