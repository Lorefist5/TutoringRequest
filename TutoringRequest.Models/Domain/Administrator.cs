using System.ComponentModel.DataAnnotations;
using TutoringRequest.Models.Domain.Base;

namespace TutoringRequest.Models.Domain;

public class Administrator : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = default!;
    public Guid InfoId { get; set; }
    public AdminAccountInfo AdminAccountInfo { get; set; }  

}
