using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;

public class AuthApiService
{
    private readonly HttpClient _httpClient;
    public List<string>? Roles { get; set; }
    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<AuthResponse> LoginAsync(LoginDto loginDto)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/login", jsonContent);
        AuthToken? authToken = null;
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        AuthResponse authResponse = new AuthResponse()
        {
            IsSuccessful = response.IsSuccessStatusCode,
            Message = await response.Content.ReadAsStringAsync(),
            Token = authToken
        };

        if (authResponse.Token != null) Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;
    }
    public async Task<AuthResponse> RegisterAsync(RegisterDto registerDto)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/register", jsonContent);
        AuthToken? authToken = null;
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        AuthResponse authResponse = new AuthResponse()
        {
            IsSuccessful = response.IsSuccessStatusCode,
            Message = await response.Content.ReadAsStringAsync(),
            Token = authToken
        };

        if (authResponse.Token != null) Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;

    }
    public async Task<bool> ForgotPassword(string email)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(new ForgotPasswordDto() { Email = email }), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("Auth/forgotpassword", jsonContent);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> ResetPassword(Guid resetTokenId, string newPassword)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(newPassword), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"Auth/resetpassword/{resetTokenId}", jsonContent);

        return response.IsSuccessStatusCode;
    }

    private IEnumerable<string> GetRolesFromJwt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if(token == null) return Enumerable.Empty<string>();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken != null)
        {
            var roles = jsonToken.Claims
                .Where(c => c.Type == "role") 
                .Select(c => c.Value);

            return roles;
        }
        else
        {
            throw new ArgumentException("Invalid JWT format");
        }
    }
    public AuthApiService AddToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return this;
    }
}
