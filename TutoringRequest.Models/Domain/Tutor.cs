using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class Tutor : BaseEntity
{
    public string TutorName { get; set; } = default!;
    public List<Course> Courses { get; set; }
    public List<TutoringSection> TutoringSections { get; set; }
    public List<AvailabilitySlot> AvailabilitySlots { get; set; }
}
