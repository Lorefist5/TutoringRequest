using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Admin;

public class AddAdministratorDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = default!;

}
