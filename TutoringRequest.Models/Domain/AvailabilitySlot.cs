using TutoringRequest.Models.Domain.Base;
using TutoringRequest.Models.Enums;
namespace TutoringRequest.Models.Domain;

public class AvailabilitySlot : BaseEntity
{
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}