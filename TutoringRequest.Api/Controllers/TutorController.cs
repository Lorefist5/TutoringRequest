using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    
    private readonly IUnitOfWork _unitOfWork;
    public TutorController(IUnitOfWork unitOfWork)
    {
        
        this._unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tutorsDto = (await _unitOfWork.TutorRepository.GetAllAsync())
            .Select(tutor => new TutorDto
            {
                Id = tutor.Id,
                TutorName = tutor.TutorName,
                StudentNumber = tutor.StudentNumber
            }).ToList();

        return Ok(tutorsDto);

    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTutor(Guid id)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == id);
        if (tutor == null) return NotFound();
        TutorDto tutorDto = new TutorDto() 
        { 
            TutorName = tutor.TutorName, 
            StudentNumber = tutor.StudentNumber,
        };
        return Ok(tutorDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTutor([FromBody] AddTutorRequest addTutorRequest)
    {
        Tutor tutor = new Tutor() 
        { 
            Id = Guid.NewGuid(),
            StudentNumber = addTutorRequest.StudentNumber, 
            TutorName = addTutorRequest.TutorName
        };
        
        await _unitOfWork.TutorRepository.AddAsync(tutor);
        await _unitOfWork.SaveChangesAsync();
        return Created();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTutor(Guid id)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == id);
        if (tutor == null) return NotFound();
        await _unitOfWork.TutorRepository.RemoveAsync(tutor);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTutor(Guid id,[FromBody] UpdateTutorRequest updateTutorRequest)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == id);
        if (tutor == null) return NotFound();
        tutor.TutorName = updateTutorRequest.TutorName;
        tutor.StudentNumber = updateTutorRequest.StudentNumber;
        await _unitOfWork.SaveChangesAsync();
        return Ok(updateTutorRequest);
    }
    //ByStudentNumber
    [HttpGet("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> GetTutorByName(string studentNumber)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();

        TutorDto tutorDto = new TutorDto()
        {
            TutorName = tutor.TutorName,
            StudentNumber = tutor.StudentNumber,
        };
        return Ok(tutorDto);
    }
    [HttpDelete("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> DeleteTutorByStudentNumber(string studentNumber)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();
        await _unitOfWork.TutorRepository.RemoveAsync(tutor);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
    [HttpPut("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> UpdateTutorByStudentName(string studentNumber, [FromBody] UpdateTutorRequest updateTutorRequest)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();
        tutor.TutorName = updateTutorRequest.TutorName;
        tutor.StudentNumber = updateTutorRequest.StudentNumber;
        await _unitOfWork.SaveChangesAsync();
        return Ok(updateTutorRequest);
    }
}
