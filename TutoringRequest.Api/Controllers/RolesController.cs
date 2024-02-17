using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.DTO.Roles;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    [HttpGet]
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
    
}
