using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class Role : BaseEntity
{
    public string RoleName { get; set; } = default!;

    public List<Account> Accounts { get; set; } = new List<Account>();
}