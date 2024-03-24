using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.WebUi.ViewModel.Change;

public class ChangeUsernameViewModel
{
    [Required]
    [Display(Name = "Current Username")]
    public string CurrentUsername { get; set; }

    [Required]
    [Display(Name = "New Username")]
    public string NewUsername { get; set; }
}
