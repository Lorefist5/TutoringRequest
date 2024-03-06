using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Api.Helpers.TokenHelpers;

public class TokenGenerator
{
    private readonly IConfiguration _configuration;
    public string Issuer
    {
        get
        {
            return _configuration["Jwt:Issuer"] ?? "https://localhost:7249";
        }
    }
    public string Audience
    {
        get
        {
            return _configuration["Jwt:Audience"] ?? "https://localhost:7249";
        }
    }
    public string Key
    {
        get
        {
            return _configuration["Jwt:Key"] ?? Guid.NewGuid().ToString();
        }
    }


    public TokenGenerator(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public string GenerateJwtToken(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Key);
        var stringfyRoles = account.Roles.Select(role => role.RoleName).ToList();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Email),
                new Claim(ClaimTypes.Role, string.Join(",", stringfyRoles)),
                new Claim("aud", Audience),
                new Claim("iss", Issuer),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public string GenerateJwtTokenForReset(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, account.Email),
            new Claim("reset", "true"),
        }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
