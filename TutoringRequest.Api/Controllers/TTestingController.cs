using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Auth;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TTestingController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TTestingController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdminAccunt([FromBody] RegisterDto newAccount)
    {
        Account account = new Account()
        {
            Name = newAccount.Name,
            Email = newAccount.Email,
            LastLogIn = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            PhoneNumber = newAccount.PhoneNumber,
            StudentNumber = newAccount.StudentNumber,
            Password = BCrypt.Net.BCrypt.HashPassword(newAccount.Password),
            Roles = new List<Role>()
            {
                _unitOfWork.RoleRepository.GetAdminRole(),
            }
        };


        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteAccountById(Guid id)
    {
        Account? acc = await _unitOfWork.AccountRepository.FirstOrDefaultAsync(a => a.Id == id);
        if (acc == null) return NotFound();

        await _unitOfWork.AccountRepository.RemoveAsync(acc);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountsTest()
    {
        var accounts = await _unitOfWork.AccountRepository.GetAllAsync();

        return Ok(accounts);
    }
}
