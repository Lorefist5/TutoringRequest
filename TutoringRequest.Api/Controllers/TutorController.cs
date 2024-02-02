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
    public IActionResult GetAll()
    {
        List<TutorDto> tutorsDto = new List<TutorDto>();
        foreach(Tutor tutor in _unitOfWork.TutorRepository.GetAll())
        {

            tutorsDto.Add(new TutorDto()
            {
                TutorName = tutor.TutorName,
                StudentNumber = tutor.StudentNumber,
                AvailabilitySlots = tutor.AvailabilitySlots.ConvertAll(
                a => new AvailabilityDto()
                {
                    Day = a.Day,
                    EndTime = a.EndTime,
                    StartTime = a.StartTime,
                    TutorId = a.TutorId
                })
            });
        }
        return Ok(tutorsDto); //Status code of 200
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetTutor( Guid id)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.Id == id);
        if (tutor == null) return NotFound();
        TutorDto tutorDto = new TutorDto() 
        { 
            TutorName = tutor.TutorName, 
            StudentNumber = tutor.StudentNumber,
            AvailabilitySlots = tutor.AvailabilitySlots.ConvertAll(
                a => new AvailabilityDto()
                {
                    Day = a.Day,
                    EndTime = a.EndTime,
                    StartTime = a.StartTime,
                    TutorId = a.TutorId
                })
        };
        return Ok(tutorDto);
    }

    [HttpPost]
    public IActionResult CreateTutor([FromBody] AddTutorRequest addTutorRequest)
    {
        Tutor tutor = new Tutor() 
        { 
            Id = Guid.NewGuid(),
            StudentNumber = addTutorRequest.StudentNumber, 
            TutorName = addTutorRequest.TutorName
        };
        
        _unitOfWork.TutorRepository.Add(tutor);
        _unitOfWork.SaveChanges();
        return Ok(tutor);
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
    //ByStudentNumber
    [HttpGet("ByStudentNumber/{studentNumber}")]
    public IActionResult GetTutorByName(string studentNumber)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();

        TutorDto tutorDto = new TutorDto()
        {
            TutorName = tutor.TutorName,
            StudentNumber = tutor.StudentNumber,
            AvailabilitySlots = tutor.AvailabilitySlots.ConvertAll(
                a => new AvailabilityDto()
                {
                    Day = a.Day,
                    EndTime = a.EndTime,
                    StartTime = a.StartTime,
                    TutorId = a.TutorId
                })
        };
        return Ok(tutorDto);
    }
    [HttpDelete("ByStudentNumber/{studentNumber}")]
    public async Task<IActionResult> DeleteTutorById(string studentNumber)
    {
        Tutor? tutor = await _unitOfWork.TutorRepository.FirstOrDefaultAsync(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();
        await _unitOfWork.TutorRepository.RemoveAsync(tutor);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
}
