namespace TutoringRequest.Models.DTO.Auth;

public class AuthResponse
{
    public string Message { get; set; } = default!;
    public bool IsSuccessful { get; set; }
    public AuthToken? Token { get; set; }
}
