using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Models.DTO.Tutors;

public class TutorDto
{
    public Guid Id { get; set; }
    public string StudentNumber { get; set; } = default!;
    public string TutorName { get; set; } = default!;
    
}
