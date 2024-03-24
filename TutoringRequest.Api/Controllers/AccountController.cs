using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Api.Helpers.TokenHelpers;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Accounts;
using TutoringRequest.Models.DTO.Roles;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly TokenGenerator _tokenGenerator;

    public AccountController(IUnitOfWork unitOfWork, IMapper mapper, TokenGenerator tokenGenerator)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
        this._tokenGenerator = tokenGenerator;
    }
    [HttpGet("roles/{id:guid}")]
    public async Task<IActionResult> GetAccountRoles(Guid id)
    {
        var account = await _unitOfWork.AccountRepository.GetAccountWithRolesAsync(a => a.Id == id);
        if(account == null) return NotFound();
        List<RoleDto> roleDtos = _mapper.Map<List<RoleDto>>(account.Roles);
        return Ok(roleDtos);
    }
    [HttpPost("roles/{accountId:guid}")]
    public async Task<IActionResult> AddRoleToAccount(Guid accountId,[FromBody] Guid roleId)
    {
        Account? account = await _unitOfWork.AccountRepository.GetAccountWithRolesAsync(a => a.Id == accountId);
        if(account == null) return NotFound();
        Role? role = await _unitOfWork.RoleRepository.FirstOrDefaultAsync(r => r.Id == roleId);
        if(role == null) return NotFound();

        if (account.Roles.Contains(role))
        {
            return BadRequest("This account contains that role already");
        }
        else
        {
            account.Roles.Add(role);    
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

    }
    [Authorize]
    [HttpGet("info/{userId:guid}")]
    public async Task<IActionResult>GetUserInfo(Guid userId)
    {
        Account? user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Id == userId);
        if(user == null) return NotFound();

        var userInfo = new AccountInfoDto()
        {
            Id = userId,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = user.Roles.Select(r => r.RoleName),
            StudentNumber = user.StudentNumber,
            UserName = user.Name
        };

        return Ok(userInfo);
    }

    [Authorize]
    [HttpPost("change-username/{userId:guid}")]
    public async Task<IActionResult> ChangeUserName(Guid userId,[FromBody] string newUserName)
    {
        Account? user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(user => user.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.Name = newUserName;
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenGenerator.GenerateJwtToken(user);


        return Ok(new { Token = token });
    }
    [Authorize]
    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmailDto)
    {
        Account? user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(user => user.Email == changeEmailDto.OldEmail);
        if (user == null)
        {
            return NotFound();
        }

        user.Email = changeEmailDto.NewEmail;
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenGenerator.GenerateJwtToken(user);


        return Ok(new { Token = token });
    }

    [Authorize]
    [HttpPost("change-phone/{userId:guid}")]
    public async Task<IActionResult> ChangePhoneNumber(Guid userId,[FromBody] string newPhoneNumber)
    {
        Account? user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(user => user.Id == userId);
        if (user == null)
        {
            return NotFound();
        }

        user.PhoneNumber = newPhoneNumber;
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenGenerator.GenerateJwtToken(user);


        return Ok(new { Token = token });
    }
    [Authorize]
    [HttpPost("change-password/{userId:guid}")]
    public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordDto changePasswordDto)
    {
        Account? user = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(user => user.Id == userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password))
        {
            return BadRequest("The old password is incorrect.");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        var token = _tokenGenerator.GenerateJwtToken(user);

        return Ok(new { Token = token });
    }

}
