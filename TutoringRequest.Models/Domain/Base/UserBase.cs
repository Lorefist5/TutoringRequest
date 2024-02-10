namespace TutoringRequest.Models.Domain.Base;

abstract public class UserBase : BaseEntity
{
    public string Email { get; set;} = default!;
    public string Password { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public DateTime LastLogIn { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
