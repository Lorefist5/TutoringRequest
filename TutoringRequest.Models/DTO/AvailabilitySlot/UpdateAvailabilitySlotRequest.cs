namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class UpdateAvailabilitySlotRequest
{
    public DayOfWeek Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}
