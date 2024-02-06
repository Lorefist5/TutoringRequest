using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> GetAll()
    {
        var tutorsDto = _mapper.Map<List<TutorDto>>(await _unitOfWork.TutorRepository.GetAllAsync());

        return Ok(tutorsDto);

    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTutorById(Guid id)
    {
        return await GetTutor(t => t.Id == id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTutor([FromBody] AddTutorRequest addTutorRequest)
    {

        Tutor tutor = _mapper.Map<Tutor>(addTutorRequest);
        Tutor? existingTutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == tutor.StudentNumber);
        if (existingTutor != null)
        {
            return Conflict(new { error = "Conflict", message = "The provided StudentNumber already exists." });
        }
        await _unitOfWork.TutorRepository.AddAsync(tutor);
        await _unitOfWork.SaveChangesAsync();

        var tutorDto = _mapper.Map<TutorDto>(tutor);
        return CreatedAtAction(nameof(GetTutorById),new { id = tutor.Id}, tutorDto);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTutorById(Guid id)
    {
        return await DeleteTutor(t => t.Id == id);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTutorById(Guid id,[FromBody] UpdateTutorRequest updateTutorRequest)
    {
        return await UpdateTutor(t => t.Id == id, updateTutorRequest);
    }
    //ByStudentNumber
    [HttpGet("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> GetTutorByName(string studentNumber)
    {
        
        return await GetTutor(t => t.StudentNumber == studentNumber);
    }
    [HttpDelete("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> DeleteTutorByStudentNumber(string studentNumber)
    {
        return await DeleteTutor(t => t.StudentNumber == studentNumber);
    }
    [HttpPut("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> UpdateTutorByStudentName(string studentNumber, [FromBody] UpdateTutorRequest updateTutorRequest)
    {
        return await UpdateTutor(t => t.StudentNumber == studentNumber, updateTutorRequest);
    }
    private async Task<IActionResult> GetTutor(Expression<Func<Tutor, bool>> predicate)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(predicate);
        if (tutor == null) return NotFound();

        TutorDto tutorDto = new TutorDto()
        {
            Id = tutor.Id,
            TutorName = tutor.TutorName,
            StudentNumber = tutor.StudentNumber,
        };
        return Ok(tutorDto);
    }
    private async Task<IActionResult> DeleteTutor(Expression<Func<Tutor, bool>> predicate)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(predicate);
        if (tutor == null) return NotFound();
        await _unitOfWork.TutorRepository.RemoveAsync(tutor);
        await _unitOfWork.SaveChangesAsync();

        TutorDto tutorDto = _mapper.Map<TutorDto>(tutor);
        return Ok(tutorDto);
    }
    private async Task<IActionResult> UpdateTutor(Expression<Func<Tutor, bool>> predicate, UpdateTutorRequest updateTutorRequest)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(predicate);
        if (tutor == null) return NotFound();
        Tutor? existingTutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == updateTutorRequest.StudentNumber && t.StudentNumber != tutor.StudentNumber);
        if(existingTutor != null) return Conflict(new { error = "Conflict", message = "The provided StudentNumber already exists." });
        tutor.TutorName = updateTutorRequest.TutorName;
        tutor.StudentNumber = updateTutorRequest.StudentNumber;
        await _unitOfWork.SaveChangesAsync();
        return Ok(updateTutorRequest);
    }
}
