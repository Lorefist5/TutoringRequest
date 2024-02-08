using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class AdminAccountInfo : BaseEntity
{   
    public DateTime LastLogIn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid AdminId { get; set; }
    public Administrator Administrator { get; set; }
    public List<AdminRole> AdminRoles { get; set; }
}
