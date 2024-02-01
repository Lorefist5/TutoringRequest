using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class AvailabilitySlot : BaseEntity
{
    public Enums.Days Day { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}