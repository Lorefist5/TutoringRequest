using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Admin;

public class AdministratorDto
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }

}
