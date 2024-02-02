namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class UpdateAvailabilitySlotRequest
{
    public Guid Id { get; set; }
    public DateTime Day { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
