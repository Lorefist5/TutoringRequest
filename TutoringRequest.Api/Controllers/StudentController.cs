using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using TutoringRequest.Data;
using TutoringRequest.Data.Repositories.Interfaces;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Students;

namespace TutoringRequest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        List<StudentDto> studentDtos = (await _unitOfWork.StudentRepository.GetAllAsync())
            .Select(s => new StudentDto() { Id = s.Id ,Name = s.Name, StudentNumber = s.StudentNumber}).ToList();
        return Ok(studentDtos);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStudentById(Guid id)
    {
        return await GetStudent(s => s.Id == id);
    }
    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] AddStudentRequest addStudentRequest)
    {
        var student = new Student { Name = addStudentRequest.Name, StudentNumber = addStudentRequest.StudentNumber };
        await _unitOfWork.StudentRepository.AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, new StudentDto { Id = student.Id, Name = student.Name, StudentNumber = student.StudentNumber });
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateStudentById(Guid id, [FromBody] UpdateStudentRequest updateStudentRequest)
    {
        return await UpdateStudent(s => s.Id == id, updateStudentRequest);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStudentById(Guid id)
    {
        return await DeleteStudent(s => s.Id == id);
    }
    //ByStudentNumber
    [HttpGet("ByStudentNumber/{studentNumber}")]
    public async Task<IActionResult> GetStudentByStudentNumber(string studentNumber)
    {
        return await GetStudent(s => s.StudentNumber == studentNumber);
    }
    [HttpPut("ByStudentNumber/{studentNumber}")]
    public async Task<IActionResult> UpdateStudentByStudentNumber(string studentNumber, [FromBody] UpdateStudentRequest updateStudentRequest)
    {
        return await UpdateStudent(s => s.StudentNumber == studentNumber, updateStudentRequest);
    }
    [HttpDelete("ByStudentNumber/{studentNumber}")]
    public async Task<IActionResult> DeleteStudentByStudentNumber(string studentNumber)
    {
        return await DeleteStudent(s => s.StudentNumber == studentNumber);
    }   
    private async Task<IActionResult> GetStudent(Expression<Func<Student, bool>> predicate)
    {
        Student? studentDomain = await _unitOfWork.StudentRepository.FirstOrDefaultAsync(predicate);
        if (studentDomain == null) return NotFound();
        var studentDto = new StudentDto() { Id = studentDomain.Id, Name = studentDomain.Name, StudentNumber = studentDomain.StudentNumber };

        return Ok(studentDto);
    }
    private async Task<IActionResult> UpdateStudent(Expression<Func<Student, bool>> predicate, UpdateStudentRequest updateStudentRequest)
    {
        Student? studentDomain = await _unitOfWork.StudentRepository.FirstOrDefaultAsync(predicate);
        if (studentDomain == null) return NotFound();
        studentDomain.Name = updateStudentRequest.Name;
        studentDomain.StudentNumber = updateStudentRequest.StudentNumber;

        await _unitOfWork.SaveChangesAsync();
        return Ok(updateStudentRequest);
    }
    private async Task<IActionResult> DeleteStudent(Expression<Func<Student, bool>> predicate)
    {
        Student? studentDomain = await _unitOfWork.StudentRepository.FirstOrDefaultAsync(predicate);
        if (studentDomain == null) return NotFound();
        _unitOfWork.StudentRepository.Remove(studentDomain);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
}
