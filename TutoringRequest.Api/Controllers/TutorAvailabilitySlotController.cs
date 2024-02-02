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
        var w = tutor.AvailabilitySlots;
        if(tutor == null) return NotFound();
        List<AvailabilitySlot> slotsDomain = await _unitOfWork.AvailabilitySlotRepository.GetTutorAvailabilitySlots(tutor);
        List<AvailabilityDto> slotsDto = slotsDomain.ConvertAll(slot => new AvailabilityDto()
        {
            Id = slot.Id,
            Day = slot.Day,
            StartTime = slot.StartTime,
            EndTime = slot.EndTime,
        });
        return Ok(slotsDto);
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
        await _unitOfWork.AvailabilitySlotRepository.AddAsync(availabilitySlotDomain);
        await _unitOfWork.SaveChangesAsync();
        

        return Created();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteTutorSlot(Guid tutorId, Guid slotId)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == tutorId);
        AvailabilitySlot? slot = await _unitOfWork.AvailabilitySlotRepository.FirstOrDefaultAsync(s => s.Id == slotId);
        if (tutor == null || slot == null) return NotFound();

        await _unitOfWork.AvailabilitySlotRepository.RemoveAsync(slot);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTutorSlot(Guid id, UpdateAvailabilitySlotRequest updateAvailabilitySlotRequest)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.Id == id);
        if (tutor == null) return NotFound();
        AvailabilitySlot? slot = tutor.AvailabilitySlots.FirstOrDefault(a => a.Id == updateAvailabilitySlotRequest.Id);
        if(slot == null) return NotFound(); 
        slot.Day = updateAvailabilitySlotRequest.Day;
        slot.StartTime = updateAvailabilitySlotRequest.StartTime;
        slot.EndTime = updateAvailabilitySlotRequest.EndTime;
         
        await _unitOfWork.SaveChangesAsync();
        return Ok();
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
    [HttpDelete("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> DeleteTutorSlotByStudentNumber(string studentNumber, Guid slotId)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        AvailabilitySlot? slot = await _unitOfWork.AvailabilitySlotRepository.FirstOrDefaultAsync(s => s.Id == slotId);
        if (tutor == null || slot == null) return NotFound();

        await _unitOfWork.AvailabilitySlotRepository.RemoveAsync(slot);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    [HttpPut("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> UpdateTutorSlotByStudentNumber(string studentNumber, UpdateAvailabilitySlotRequest updateAvailabilitySlotRequest)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();
        AvailabilitySlot? slot = tutor.AvailabilitySlots.FirstOrDefault(a => a.Id == updateAvailabilitySlotRequest.Id);
        if (slot == null) return NotFound();
        slot.Day = updateAvailabilitySlotRequest.Day;
        slot.StartTime = updateAvailabilitySlotRequest.StartTime;
        slot.EndTime = updateAvailabilitySlotRequest.EndTime;

        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
}
