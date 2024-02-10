using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.Models.DTO.Tutors;

public class AddTutorDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "StudentNumber is required")]
    public string StudentNumber { get; set; } = default!;
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;
}
