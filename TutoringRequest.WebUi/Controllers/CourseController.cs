using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Majors;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.ViewModel;

namespace TutoringRequest.WebUi.Controllers;
public class CourseController : Controller
{
    private readonly CourseApiService _courseApiService;
    private readonly MajorApiService _majorApiService;

    public CourseController(CourseApiService courseApiService, MajorApiService majorApiService)
    {
        this._courseApiService = courseApiService;
        this._majorApiService = majorApiService;
    }
    public async Task<IActionResult> Index(Guid? majorId, string searchQuery = "")
    {
        var coursesResponse = majorId.HasValue ?
            await _majorApiService.GetCoursesByMajor(majorId.Value) :
            await _courseApiService.GetAllAsync<CourseDto>(); //If a major was selected as a filter it will get all the courses from that major if not it will get all the courses
        var majorsResponse = await _majorApiService.GetAllAsync<MajorDto>();

        if (!coursesResponse.IsSuccess || !majorsResponse.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Majors = majorsResponse.Values!;
        ViewBag.SelectedMajorId = majorId;
        List<CourseDto> courses = coursesResponse.Values!;
        List<MajorDto> majors = majorsResponse.Values!;

        // Filter courses by the search query if it's not empty
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            courses = courses.Where(c => c.CourseName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        IEnumerable<CourseViewModel> coursesViewModel = courses.Select(course =>
        {
            MajorDto? major = majors.FirstOrDefault(m => m.Id == course.MajorId);
            return new CourseViewModel(course, major);
        });

        return View(coursesViewModel);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(CourseDto courseDto)
    {
        return View(courseDto);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var response = await _courseApiService.DeleteAsync(id.ToString());
        if (!response.IsSuccessStatusCode)
        {
            return View();
        }
        TempData["SuccessMessage"] = "Course deleted successfully!";
        return RedirectToAction("Index", "Course");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Guid? majorId)
    {
        var response = await _majorApiService.GetAllAsync<MajorDto>();
        if (!response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }
        if(majorId != null)
        {
            ViewBag.SelectedMajorId = majorId;
        }
        var viewModel = new AddCourseViewModel
        {
            Majors = response.Values!
        };

        return View(viewModel);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(AddCourseViewModel courseViewModel)
    {
        var response = await _courseApiService.PostAsync(courseViewModel.AddCourseDto);
        if (!response.IsSuccessStatusCode)
        {
            return View();
        }
        TempData["SuccessMessage"] = "Course created successfully!";
        return RedirectToAction("Index", "Course");
    }
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Schedule()
    {
        return View();
    }
}
