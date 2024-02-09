using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringRequest.Models.Domain;

namespace TutoringRequest.Models.DTO.Courses;

public class UpdateCourseDto
{
    public string CourseName { get; set; } = default!;
    public Guid MajorId { get; set; }
    
}
