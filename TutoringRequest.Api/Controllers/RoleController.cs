using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutoringRequest.Api.Helpers.ApiHelpers;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.DTO.Roles;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _unitOfWork.RoleRepository.GetAllAsync();
        List<RoleDto> roleDtos = new List<RoleDto>();
        foreach (var role in roles)
        {
            roleDtos.Add(new RoleDto() { RoleName = role.RoleName});
        }
        return Ok(roleDtos);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRole(AddRoleDto addRoleRequest)
    {
        Guid? currentUserId = SectionHelper.GetCurrentUserId(this);
        if (currentUserId == null) return BadRequest("Current user not autheticated");
        if (string.IsNullOrWhiteSpace(addRoleRequest.RoleName)) return BadRequest("Role name is required");
        await _unitOfWork.RoleRepository.AddAsync(new Models.Domain.Role() { CreatedById = currentUserId, RoleName = addRoleRequest.RoleName});

        return Ok(addRoleRequest);
    }
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteRole(Guid guid)
    {
        Models.Domain.Role? existingRole = await _unitOfWork.RoleRepository.FirstOrDefaultAsync(r => r.Id == guid);
        if (existingRole == null) return NotFound();
        await _unitOfWork.RoleRepository.RemoveAsync(existingRole);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
}
