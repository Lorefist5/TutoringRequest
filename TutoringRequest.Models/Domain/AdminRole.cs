using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class AdminRole : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

}