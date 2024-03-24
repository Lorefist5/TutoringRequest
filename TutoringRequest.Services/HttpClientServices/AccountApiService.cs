using System.Text.Json;
using System.Text;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;
using TutoringRequest.Models.DTO.Accounts;
using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.DTO.Auth;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;
using TutoringRequest.Models.DTO.Http;


namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Account")]
public class AccountApiService : GenericApiService
{
    public AccountApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string? defaultRoute = null) : base(httpClient, httpContextAccessor, defaultRoute)
    {
    }
    public async Task<HttpResponse<AccountInfoDto>> GetAccountInfoAsync(Guid id)
    {
        var requestMessage = CreateRequestMessage(HttpMethod.Get, $"{_defaultRoute}/info/{id}");
        var response = await _httpClient.SendAsync(requestMessage);

        var httpResponse = new HttpResponse<AccountInfoDto>();

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var accountInfo = JsonSerializer.Deserialize<AccountInfoDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (accountInfo != null)
            {
                httpResponse.IsSuccess = true;
                httpResponse.Value = accountInfo;
            }
            else
            {
                httpResponse.IsSuccess = false;
            }
        }
        else
        {
            httpResponse.IsSuccess = false;
        }

        return httpResponse;
    }
    public async Task<AuthResponse> ChangePhoneAsync(Guid id, string phoneNumber)
    {
        AuthToken? authToken = null;
        var jsonContent = JsonSerializer.Serialize(phoneNumber);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var requestMessage = CreateRequestMessage(HttpMethod.Post, _defaultRoute + "/change-phone/" + id.ToString(), content);

        var response = await _httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        var authResponse = new AuthResponse() { Token = authToken, IsSuccessful = response.IsSuccessStatusCode, Message = await response.Content.ReadAsStringAsync() };
        if (authResponse.Token != null) authResponse.Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;
    }
    public async Task<AuthResponse> ChangePasswordAsync(Guid id, ChangePasswordDto changePasswordDto)
    {
        AuthToken? authToken = null;
        var jsonContent = JsonSerializer.Serialize(changePasswordDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var requestMessage = CreateRequestMessage(HttpMethod.Post, _defaultRoute + "/change-password/"+id.ToString(), content);

        var response = await _httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        var authResponse = new AuthResponse() { Token = authToken, IsSuccessful = response.IsSuccessStatusCode, Message = await response.Content.ReadAsStringAsync() };
        if (authResponse.Token != null) authResponse.Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;
    }
    public async Task<AuthResponse> ChangeEmailAsync(ChangeEmailDto emailDto)
    {
        AuthToken? authToken = null;
        var jsonContent = JsonSerializer.Serialize(emailDto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        var requestMessage = CreateRequestMessage(HttpMethod.Post, _defaultRoute + "/change-email", content);

        var response = await _httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        var authResponse = new AuthResponse() { Token = authToken, IsSuccessful = response.IsSuccessStatusCode, Message = await response.Content.ReadAsStringAsync() };
        if (authResponse.Token != null) authResponse.Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;
    }
    public async Task<AuthResponse> ChangeUserNameAsync(Guid userId, string newUserName)
    {
        AuthToken? authToken = null;
        var jsonContent = JsonSerializer.Serialize(newUserName);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var requestMessage = CreateRequestMessage(HttpMethod.Post, _defaultRoute + "/change-username/"+ userId.ToString(), content);

        var response = await _httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode) authToken = await response.Content.ReadFromJsonAsync<AuthToken>();
        var authResponse = new AuthResponse() { Token = authToken, IsSuccessful = response.IsSuccessStatusCode, Message = await response.Content.ReadAsStringAsync() };
        if (authResponse.Token != null) authResponse.Roles = GetRolesFromJwt(authResponse.Token.Token).ToList();
        return authResponse;
    }
    private IEnumerable<string> GetRolesFromJwt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (token == null) return Enumerable.Empty<string>();
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
}
