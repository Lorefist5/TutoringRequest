using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Admin;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Controllers;

// TutorController
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TutorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TutorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTutors()
    {
        var tutors = _mapper.Map<List<TutorDto>>(await _unitOfWork.AccountRepository.TutorRepository.GetAllAsync());
        return Ok(tutors);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTutorById(Guid id)
    {

        return await GetTutor(a => a.Id == id);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTutor([FromBody] AddTutorDto addTutorDto)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(addTutorDto.Password);
        var roles = new List<Role>()
        {
            _unitOfWork.RoleRepository.GetStudentRole(),
            _unitOfWork.RoleRepository.GetTutorRole()
        };

        Account account = new Account()
        {
            CreatedAt = DateTime.UtcNow,
            Email = addTutorDto.Email,
            LastLogIn = DateTime.UtcNow,
            Name = addTutorDto.Name,
            Password = hashedPassword,
            PhoneNumber = addTutorDto.PhoneNumber,
            StudentNumber = addTutorDto.StudentNumber,
            Roles = roles
        };

        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.SaveChangesAsync();
        return Created();
    }
    private async Task<IActionResult> GetTutor(Expression<Func<Account, bool>> predicate)
    {
        var tutor = await _unitOfWork.AccountRepository.TutorRepository.FirstOrDefaultAsync(predicate);
        if (tutor == null) return NotFound();
        return Ok(tutor);
    }
}
