using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.Models.DTO.Auth;
namespace TutoringRequest.WebUi.Services;

public class UserAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAuthenticationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> SignInUserAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null) return false;

        // Assuming "nameid" and "unique_name" are the claims used; adjust as necessary.
        var email = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
        var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
        var userName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value;
        if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userName)) return false;

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        identity.AddClaim(new Claim(ClaimTypes.Name, userName));
        identity.AddClaim(new Claim(ClaimTypes.Email, email));
        foreach (var role in GetRolesFromJwt(token))
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var principal = new ClaimsPrincipal(identity);
        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        // Assuming the token is to be stored as a cookie; adjust storage method as needed
        _httpContextAccessor.HttpContext.Response.Cookies.Append("LoginCookie", token, new CookieOptions { HttpOnly = true, Secure = _httpContextAccessor.HttpContext.Request.IsHttps });

        return true;
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
