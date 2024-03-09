using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Roles;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
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

}
