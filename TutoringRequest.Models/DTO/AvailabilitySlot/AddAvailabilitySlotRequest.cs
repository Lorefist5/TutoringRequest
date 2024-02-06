using System.ComponentModel.DataAnnotations;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.AvailabilitySlot;

public class AddAvailabilitySlotRequest
{
    [Required]
    public DayOfWeek Day { get; set; }
    [Required]
    public TimeSpan StartTime { get; set; }
    [Required]
    public TimeSpan EndTime { get; set; }

}
