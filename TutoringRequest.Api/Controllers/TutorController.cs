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

    public TutorController(ITutorRepository tutorRepository)
    {
        this._tutorRepository = tutorRepository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_tutorRepository.GetAll());
    }
    [HttpPost]
    public IActionResult Create([FromBody] AddTutorRequest addTutorRequest)
    {
        Tutor tutor = new Tutor() { Id = Guid.NewGuid(), TutorName = addTutorRequest.TutorName};
        
        _tutorRepository.Add(tutor);
        return Ok(tutor);
    }
}
