namespace TutoringRequest.Models.DTO.Students;

public class StudentDto
{
    public Guid Id { get; set; }
    public string StudentNumber { get; set; } = default!;
    public string Name { get; set; } = default!;
}
