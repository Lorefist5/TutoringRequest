using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Collections.Generic;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Admin;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministratorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdministratorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAdmins()
    {
        List<AdministratorDto> adminDtos = _mapper.Map<List<AdministratorDto>>(await _unitOfWork.AdministratorRepository.GetAllAsync());
        return Ok(adminDtos);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] AddAdministratorDto addAdministratorDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(addAdministratorDto.Password);
        var admin = _mapper.Map<Administrator>(addAdministratorDto);
        admin.Password = hashedPassword;
        var accountInfo = new AdminAccountInfo()
        {
            AdminId = admin.Id,
            Administrator = admin,
            CreatedAt = DateTime.Now,
            LastLogIn = DateTime.Now
        };
        admin.AdminAccountInfo = accountInfo;
        admin.InfoId = accountInfo.Id;
        await _unitOfWork.AdministratorRepository.AddAsync(admin);
        await _unitOfWork.AdminAccountInfoRepository.AddAsync(accountInfo);

        await _unitOfWork.SaveChangesAsync();

        return Created();
    }
}
