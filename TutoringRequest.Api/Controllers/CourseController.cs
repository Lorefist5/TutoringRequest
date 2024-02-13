using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Courses;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        return Ok(_mapper.Map<List<CourseDto>>(await _unitOfWork.CourseRepository.GetAllAsync()));
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
        return await GetCourse(c => c.Id == id);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCourse([FromBody] AddCourseDto addCourseDto)
    {
        if (ModelState.IsValid)
        {
            Major? major = await _unitOfWork.MajorRepository.FirstOrDefaultAsync(m => m.Id == addCourseDto.MajorId);
            if (major == null) return NotFound();
            Course course = _mapper.Map<Course>(addCourseDto);
            await _unitOfWork.CourseRepository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourseById), new {id =  course.Id}, addCourseDto);
        }
        return BadRequest(ModelState);

    }
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCourseById(Guid id, [FromBody]UpdateCourseDto updateCourseDto)
    {
        return await UpdateCourse(c => c.Id == id, updateCourseDto);
    }
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        return await DeleteCourse(c => c.Id == id);
    }

    private async Task<IActionResult> GetCourse(Expression<Func<Course, bool>> predicate)
    {
        Course? course = await _unitOfWork.CourseRepository.FirstOrDefaultAsync(predicate);
        if (course == null) return NotFound();
        CourseDto courseDto = _mapper.Map<CourseDto>(course);
        return Ok(courseDto);
    }
    private async Task<IActionResult> UpdateCourse(Expression<Func<Course, bool>> predicate, UpdateCourseDto updateCourseDto)
    {
        if (ModelState.IsValid)
        {
            Course? course = await _unitOfWork.CourseRepository.FirstOrDefaultAsync(predicate);
            if(course == null) return NotFound();   
            Major? major = await _unitOfWork.MajorRepository.FirstOrDefaultAsync(m => m.Id == updateCourseDto.MajorId);
            if (major == null) return NotFound();
            course.CourseName = updateCourseDto.CourseName;
            course.MajorId = updateCourseDto.MajorId;

            CourseDto courseDto = _mapper.Map<CourseDto>(course);
            return Ok(courseDto);
        }
        return BadRequest(ModelState);

    }
    private async Task<IActionResult> DeleteCourse(Expression<Func<Course, bool>> predicate)
    {
        Course? course = await _unitOfWork.CourseRepository.FirstOrDefaultAsync(predicate);
        if(course == null) return NotFound();
        CourseDto courseDto = _mapper.Map<CourseDto>(course);
        await _unitOfWork.CourseRepository.RemoveAsync(course);
        await _unitOfWork.SaveChangesAsync();
        return Ok(courseDto);
    }
}
