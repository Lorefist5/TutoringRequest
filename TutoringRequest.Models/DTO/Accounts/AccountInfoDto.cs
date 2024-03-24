namespace TutoringRequest.Models.DTO.Accounts;

public class AccountInfoDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }   
    public string? PhoneNumber { get; set; }
    public string? StudentNumber { get; set; }
    public IEnumerable<string> Roles { get; set; }

}
