using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Tutors;

public class TutorDto
{
    public string StudentNumber { get; set; } = default!;
    public string TutorName { get; set; } = default!;
    public List<Domain.AvailabilitySlot> AvailabilitySlots { get; set; }
}
