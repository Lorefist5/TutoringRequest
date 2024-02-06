
using TutoringRequest.Models.Domain.Base;
namespace TutoringRequest.Models.Domain;

public class Student : BaseEntity
{
    public string StudentNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<TutoringSection> TutoringSections { get; set; }
}
