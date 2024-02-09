using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.AvailabilitySlot;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorAvailabilitySlotController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TutorAvailabilitySlotController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<List<AvailabilityDto>>> GetTutorSlotsById(Guid tutorId)
    {
        return await GetSlots(t => t.Id == tutorId);
    }
    [HttpPost]
    public async Task<IActionResult> CreateTutorSlotById([FromHeader] Guid tutorId, [FromBody] AddAvailabilitySlotRequest addAvailabilitySlotRequest)
    {
        return await CreateSlot(t => t.Id == tutorId, addAvailabilitySlotRequest);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTutorSlotById(Guid id)
    {
        return await DeleteSlot(s => s.Id == id);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSlotById(Guid id, [FromBody] UpdateAvailabilitySlotRequest updateAvailabilitySlotRequest)
    {
        return await UpdateSlot(s => s.Id == id, updateAvailabilitySlotRequest);
    }
    //ByTutorStudentNumber
    [HttpGet("ByTutorStudentNumber/{studentNumber}")]
    public async Task<ActionResult<List<AvailabilityDto>>> GetTutorSlotsByStudentNumber(string studentNumber)
    {
        return await GetSlots(t => t.StudentNumber == studentNumber);
    }
    [HttpPost("ByTutorStudentNumber/{studentNumber}")]
    public async Task<IActionResult> CreateTutorSlotByStudentNumber(string studentNumber, [FromBody] AddAvailabilitySlotRequest addAvailabilitySlotRequest)
    {
        return await CreateSlot(t => t.StudentNumber == studentNumber, addAvailabilitySlotRequest);
    }
    private async Task<IActionResult> CreateSlot(Expression<Func<Tutor, bool>> predicate, AddAvailabilitySlotRequest addAvailabilitySlotRequest)
    {
        if (ModelState.IsValid)
        {
            Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(predicate);
            if (tutor == null) return NotFound();

            AvailabilitySlot availabilitySlotDomain = _mapper.Map<AvailabilitySlot>(addAvailabilitySlotRequest);
            availabilitySlotDomain.Tutor = tutor;
            // Check for slot overlap before creating a new slot
            if (await CheckForSlotOverlap(tutor,availabilitySlotDomain))
            {
                return Conflict("The new slot overlaps with an existing slot.");
            }


            await _unitOfWork.AvailabilitySlotRepository.AddAsync(availabilitySlotDomain);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTutorSlotsById), new { tutorId = tutor.Id }, addAvailabilitySlotRequest);
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    private async Task<ActionResult<List<AvailabilityDto>>> GetSlots(Expression<Func<Tutor, bool>> predicate)
    {
        var tutors = await _unitOfWork.TutorRepository.GetAllAsync();
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(predicate);

        if (tutor == null) return NotFound();
        List<AvailabilityDto> availabilityDtos = _mapper.Map<List<AvailabilityDto>>(await _unitOfWork.AvailabilitySlotRepository
            .GetTutorAvailabilitySlotsAsync(tutor));

        return Ok(availabilityDtos);
    }
    private async Task<IActionResult> DeleteSlot(Expression<Func<AvailabilitySlot, bool>> slotExpression)
    {
        
        AvailabilitySlot? slot = await _unitOfWork.AvailabilitySlotRepository.FirstOrDefaultAsync(slotExpression);
        if(slot == null) return NotFound();
        await _unitOfWork.AvailabilitySlotRepository.RemoveAsync(slot);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    private async Task<IActionResult> UpdateSlot(Expression<Func<AvailabilitySlot, bool>> slotExpression, UpdateAvailabilitySlotRequest updateAvailabilitySlotRequest)
    {
        AvailabilitySlot? slot = await _unitOfWork.AvailabilitySlotRepository.FirstOrDefaultAsync(slotExpression);
        if (slot == null) return NotFound();
        
        slot.Day = updateAvailabilitySlotRequest.Day;
        slot.StartTime = updateAvailabilitySlotRequest.StartTime;
        slot.EndTime = updateAvailabilitySlotRequest.EndTime;
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
    private async Task<bool> CheckForSlotOverlap(Tutor tutor, AvailabilitySlot availabilitySlot)
    {
        // Check for slots that overlap with the new slot
        var overlappingSlots = await _unitOfWork.AvailabilitySlotRepository
            .WhereAsync(slot => tutor.Id == slot.TutorId &&
                ((availabilitySlot.StartTime >= slot.StartTime && availabilitySlot.StartTime <= slot.EndTime) ||
                 (availabilitySlot.EndTime >= slot.StartTime && availabilitySlot.EndTime <= slot.EndTime)));





        overlappingSlots = overlappingSlots.Where(slot => !(availabilitySlot.StartTime <= slot.StartTime && availabilitySlot.EndTime >= slot.EndTime)).ToList();

        // Return true if there are any overlapping slots
        return overlappingSlots.Any();
    }
}
