using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class AvailabilityDto
{
    public Guid Id { get; set; }
    public Guid TutorId { get; set; }
    public DateTime Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
