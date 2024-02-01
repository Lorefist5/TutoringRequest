using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Tutors;

public class AddTutorRequest
{
    public string TutorName { get; set; } = default!;
    public List<AvailabilitySlot> AvailabilitySlots { get; set; }
}
