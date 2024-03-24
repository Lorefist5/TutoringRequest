namespace TutoringRequest.Models.DTO.Accounts;

public class ChangeUserNameDto
{
    public Guid UserId { get; set; }
    public string NewUserName { get; set; }
}
