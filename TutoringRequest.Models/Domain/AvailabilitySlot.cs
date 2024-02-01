using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class AvailabilitySlot : BaseEntity
{
    public Guid TutorId { get; set; }
    public Tutor Tutor { get; set; }
    public DateTime Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}