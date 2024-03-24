using System.ComponentModel.DataAnnotations;

namespace TutoringRequest.WebUi.ViewModel.Change;

public class ChangePhoneViewModel
{
    [Required]
    [Display(Name = "Old Phone Number")]
    public string PhoneOldNumber { get; set; }

    [Required]
    [Display(Name = "New Phone Number")]
    [Compare("PhoneOldNumber", ErrorMessage = "The new phone number must be different from the old phone number.")]
    public string PhoneNewNumber { get; set; }
}
