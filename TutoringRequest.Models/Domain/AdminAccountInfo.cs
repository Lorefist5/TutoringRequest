using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class AdminAccountInfo : BaseEntity
{
    public Guid AdminId { get; set; }
    public Administrator Administrator { get; set; }
    public Guid RoleId { get; set; }
    public AdminRole Role { get; set; }
    
    public DateTime LastLogIn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
