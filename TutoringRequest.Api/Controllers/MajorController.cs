using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MajorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MajorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<MajorDto>>> GetMajors()
    {
        List<MajorDto> majorDtos = _mapper.Map<List<MajorDto>>(await _unitOfWork.MajorRepository.GetAllAsync());
        return Ok(majorDtos);
    }
    [HttpPost]
    public async Task<ActionResult<MajorDto>> CreateMajor([FromBody] AddMajorDto addMajorDto)
    {
        var majorDomain = _mapper.Map<Major>(addMajorDto);
        await _unitOfWork.MajorRepository.AddAsync(majorDomain);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMajorById), new { id =  majorDomain.Id }, addMajorDto);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MajorDto>> GetMajorById(Guid id)
    {
        return await GetMajor(major => major.Id == id);
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateMajorDto>> UpdateMajorById(Guid id, [FromBody] UpdateMajorDto updateMajorDto)
    {
        return await UpdateMajor(major => major.Id == id, updateMajorDto);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMajor(Guid id)
    {
        Major? major = _unitOfWork.MajorRepository.FirstOrDefault(m => m.Id == id);
        if(major == null) return NotFound();

        await _unitOfWork.MajorRepository.RemoveAsync(major);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }
    //By name 
    [HttpGet("ByMajorName/{majorName}")]
    public async Task<ActionResult<MajorDto>> GetMajorByName(string majorName)
    {
        return await GetMajor(major => major.MajorName == majorName);
    }
    //Gets all the courses in the major
    [HttpGet("{id:guid}/courses")]
    public async Task<IActionResult> GetCoursesInMajor(Guid id)
    {
        Major? major = await _unitOfWork.MajorRepository.FirstOrDefaultAsync(m => m.Id == id);
        if (major == null) return NotFound();
        List<CourseDto> courseDtos = _mapper.Map<List<CourseDto>>(await _unitOfWork.MajorRepository.GetCoursesInMajorAsync(major));
        return Ok(courseDtos);
    }
    private async Task<ActionResult<MajorDto>> GetMajor(Expression<Func<Major, bool>> predicate)
    {
        Major? major = await _unitOfWork.MajorRepository.FirstOrDefaultAsync(predicate);
        if (major == null) return NotFound();
        MajorDto majorDto = _mapper.Map<MajorDto>(major);
        return Ok(majorDto);
    }
    private async Task<ActionResult<UpdateMajorDto>> UpdateMajor(Expression<Func<Major, bool>> predicate, UpdateMajorDto updateMajorDto)
    {
        Major? major = await _unitOfWork.MajorRepository.FirstOrDefaultAsync(predicate);
        if (major == null) return NotFound();
        major.MajorName = updateMajorDto.MajorName;
        await _unitOfWork.SaveChangesAsync();
        return Ok(updateMajorDto);
    }

}
