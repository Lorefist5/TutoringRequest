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
    public IActionResult GetTutorSlots(Guid tutorId)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.Id == tutorId);
        if(tutor == null) return NotFound();

        return Ok(tutor.AvailabilitySlots);
    }
    [HttpPost]
    public IActionResult CreateTutorSlot([FromHeader] Guid tutorId, [FromBody] AddAvailabilitySlotRequest availabilitySlotDto)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.Id == tutorId);
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
    [HttpPost("ByTutorStudentNumber/{studentNumber}")]
    public IActionResult CreateTutorSlotByStudentNumber(string studentNumber,[FromBody] AddAvailabilitySlotRequest availabilitySlotDto)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.StudentNumber == studentNumber);
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
        _unitOfWork.AvailabilitySlotRepository.Add(availabilitySlotDomain);
        _unitOfWork.SaveChanges();


        return Created();
    }
}
