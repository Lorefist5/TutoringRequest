using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TutoringRequest.Models.DTO.Auth;

namespace TutoringRequest.Services.HttpClientServices;

public class AuthApiService
{
    private readonly HttpClient _httpClient;

    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/login", jsonContent);


        if (!response.IsSuccessStatusCode)
        {
            return "";
        }

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();
        if (token == null)
        {
            return "";
        }

        return token.Token;
    }
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/register", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            return "";
        }

        var token = await response.Content.ReadFromJsonAsync<AuthToken>();
        if (token == null)
        {
            return "";
        }

        return token.Token;
    }
    public async Task<bool> ForgotPassword(string email)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(new ForgotPasswordDto() { Email = email}), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("Auth/forgotpassword", jsonContent);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> ResetPassword(Guid resetTokenId,string newPassword)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(newPassword), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/resetpassword/{resetTokenId}", jsonContent);

        return response.IsSuccessStatusCode;
    }


}
