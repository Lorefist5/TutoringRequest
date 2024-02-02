using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Models.DTO.Tutors;

public class TutorDto
{
    public string StudentNumber { get; set; } = default!;
    public string TutorName { get; set; } = default!;
    public List<AvailabilityDto> AvailabilitySlots { get; set; }
}
