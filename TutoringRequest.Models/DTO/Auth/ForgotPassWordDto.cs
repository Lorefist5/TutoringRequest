using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.Models.DTO.Auth;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
}
