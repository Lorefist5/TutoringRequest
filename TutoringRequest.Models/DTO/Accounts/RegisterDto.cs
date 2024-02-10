using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.Models.DTO.Accounts;

public class RegisterDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    public string? StudentNumber { get; set; }

    [Phone(ErrorMessage = "Invalid phone number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    public string? AuthToken { get; set; }
}
