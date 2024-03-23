﻿using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;

namespace TutoringRequest.WebUi.ViewModel;

public class CourseViewModel
{
    public CourseViewModel(CourseDto course, MajorDto major)
    {
        Course = course;
        Major = major;
    }
    public CourseDto Course { get; set; }
    public MajorDto Major { get; set; }
}
