using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Accounts;
using Microsoft.IdentityModel.Tokens;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        // Validate the input data
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the email is already registered
        if (_unitOfWork.AccountRepository.FirstOrDefault(a => a.Email == registerDto.Email) != null)
        {
            return BadRequest("Email is already registered");
        }

        // Hash the password
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // Generate a JWT token
        //var authToken = GenerateJwtToken(registerDto.Email);

        // Create an instance of the Account model
        var account = new Account
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = hashedPassword,
            PhoneNumber = registerDto.PhoneNumber,
            StudentNumber = registerDto.StudentNumber,
            CreatedAt = DateTime.UtcNow,
            LastLogIn = DateTime.UtcNow,
            Roles = new List<Role> { _unitOfWork.RoleRepository.GetStudentRole() },
          //  AuthToken = authToken
        };

        // Add the new account to the database
        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        throw new NotImplementedException();
        //return CreatedAtAction(nameof(Register), new { AuthToken = authToken });
    }

}
