using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class AddAvailabilitySlotRequest
{
    public DateTime Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
