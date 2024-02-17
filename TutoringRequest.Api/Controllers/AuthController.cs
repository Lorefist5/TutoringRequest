using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Api.Services.Interfaces;
using TutoringRequest.Services.Interfaces;
using TutoringRequest.Helpers.TokenHelpers;

namespace TutoringRequest.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenGenerator _tokenGenerator;
    private readonly IEmailService _emailService;
    private readonly IResetTokenService _resetTokenService;

    public AuthController(IUnitOfWork unitOfWork, TokenGenerator tokenGenerator, IEmailService emailService, IResetTokenService resetTokenService)
    {
        _unitOfWork = unitOfWork;
        this._tokenGenerator = tokenGenerator;
        this._emailService = emailService;
        this._resetTokenService = resetTokenService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        try
        {
            // Check if the email is already registered
            var existingUser = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Email == dto.Email || a.StudentNumber == dto.StudentNumber);
            if (existingUser != null)
            {
                return BadRequest("Email or student number are already registered.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var roles = new List<Role>()
            {
                _unitOfWork.RoleRepository.GetStudentRole()
            };

            var newUser = new Account
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = hashedPassword,
                StudentNumber = dto.StudentNumber,
                CreatedAt = DateTime.UtcNow,
                LastLogIn = DateTime.UtcNow,
                Roles = roles
            };

            await _unitOfWork.AccountRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            var token = _tokenGenerator.GenerateJwtToken(newUser);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var user = await _unitOfWork.AccountRepository.GetAccountWithRolesAsync(a => a.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _tokenGenerator.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    [AllowAnonymous]
    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        try
        {
            var user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Email == model.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            ResetToken resetToken = await _resetTokenService.CreateResetTokenAsync(user.Id);
            await _emailService.SendResetEmail(model.Email, resetToken);

            return Ok("Password reset email sent.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    [AllowAnonymous]
    [HttpPost("resetpassword/{resetTokenId:guid}")]
    public async Task<IActionResult> ResetPassword(Guid resetTokenId, [FromBody] string newPassword)
    {
        try
        {
            // Validate reset token ID and model
            if (resetTokenId == Guid.Empty || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Invalid reset token ID or missing new password.");
            }

            // Fetch the reset token by ID
            var resetToken = await _unitOfWork.ResetTokenRepository.GetTokenByUniqueIdentifierAsync(resetTokenId);
            if (resetToken == null || !resetToken.IsActive || resetToken.ExpirationTime < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired reset token.");
            }

            // Fetch the associated user
            var user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Id == resetToken.AccountId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }


            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _unitOfWork.SaveChangesAsync();


            resetToken.IsActive = false;
            await _unitOfWork.SaveChangesAsync();


            return Ok("Password reset successful.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }


}
