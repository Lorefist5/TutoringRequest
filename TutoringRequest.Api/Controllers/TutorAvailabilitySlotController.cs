using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorAvailabilitySlotController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TutorAvailabilitySlotController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> GetTutorSlots(Guid tutorId)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == tutorId);
        if(tutor == null) return NotFound();

        return Ok(tutor.AvailabilitySlots);
    }
    [HttpPost]
    public async Task<IActionResult> CreateTutorSlot([FromHeader] Guid tutorId, [FromBody] AddAvailabilitySlotRequest availabilitySlotDto)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == tutorId);
        if (tutor == null) return NotFound();

        AvailabilitySlot availabilitySlotDomain = new AvailabilitySlot()
        {
            Day = availabilitySlotDto.Day,
            EndTime = availabilitySlotDto.EndTime,
            Id = Guid.NewGuid(),
            StartTime = availabilitySlotDto.StartTime,
            TutorId = tutorId,
            Tutor = tutor
        };
        _unitOfWork.AvailabilitySlotRepository.Add(availabilitySlotDomain);
        _unitOfWork.SaveChanges();
        

        return Created();
    }
    //ByTutorStudentNumber
    [HttpGet("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> GetTutorSlotsByStudentNumber(string studentNumber)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();

        return Ok(tutor.AvailabilitySlots);
    }
    [HttpPost("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> CreateTutorSlotByStudentNumber(string studentNumber,[FromBody] AddAvailabilitySlotRequest availabilitySlotDto)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();

        AvailabilitySlot availabilitySlotDomain = new AvailabilitySlot()
        {
            Day = availabilitySlotDto.Day,
            EndTime = availabilitySlotDto.EndTime,
            Id = Guid.NewGuid(),
            StartTime = availabilitySlotDto.StartTime,
            TutorId = tutor.Id,
            Tutor = tutor
        };
        await _unitOfWork.AvailabilitySlotRepository.AddAsync(availabilitySlotDomain);
        await _unitOfWork.SaveChangesAsync();


        return Created();
    }

}
