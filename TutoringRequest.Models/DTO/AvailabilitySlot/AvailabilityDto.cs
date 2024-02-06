using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class AvailabilityDto
{
    public Guid Id { get; set; }
    public Guid TutorId { get; set; }
    public string Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
