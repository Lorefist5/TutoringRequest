using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class Account : UserBase
{
    public string Name { get; set; } = default!;
    public string? StudentNumber { get; set; }
    public List<Role> Roles { get; set; } = new List<Role>();
}
