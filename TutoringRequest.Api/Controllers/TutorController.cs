using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IUnitOfWork _unitOfWork;
    public TutorController(IUnitOfWork unitOfWork)
    {
        this._tutorRepository = unitOfWork.TutorRepository;
        this._unitOfWork = unitOfWork;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_tutorRepository.GetAll()); //Status code of 200
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetTutor( Guid id)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.Id == id);
        if (tutor == null) return NotFound();
        TutorDto tutorDto = new TutorDto() { TutorName = tutor.TutorName, StudentNumber = tutor.StudentNumber, AvailabilitySlots = tutor.AvailabilitySlots};
        return Ok(tutorDto);
    }
    [HttpGet("byStudentNumber")]
    public IActionResult GetTutorByName([FromHeader] string studentNumber)
    {
        Tutor? tutor = _unitOfWork.TutorRepository.FirstOrDefault(t => t.StudentNumber == studentNumber);
        if (tutor == null) return NotFound();
        TutorDto tutorDto = new TutorDto() { TutorName = tutor.TutorName, StudentNumber = tutor.StudentNumber, AvailabilitySlots = tutor.AvailabilitySlots };
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
        
        _tutorRepository.Add(tutor);
        _unitOfWork.SaveChanges();
        return Ok(tutor);
    }
}
