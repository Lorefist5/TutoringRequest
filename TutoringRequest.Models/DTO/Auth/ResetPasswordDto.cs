using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.Models.DTO.Auth;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "UserId is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "ResetToken is required")]
    public string ResetToken { get; set; }

    [Required(ErrorMessage = "NewPassword is required")]
    public string NewPassword { get; set; }
}
