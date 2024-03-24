using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.WebUi.ViewModel.Change;

public class ChangeEmailViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Current Email")]
    public string CurrentEmail { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "New Email")]
    public string NewEmail { get; set; }

    [EmailAddress]
    [Compare("NewEmail", ErrorMessage = "The new email and confirmation email do not match.")]
    [Display(Name = "Confirm New Email")]
    public string ConfirmNewEmail { get; set; }
}
