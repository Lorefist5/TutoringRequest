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
    public async Task<IActionResult> Index()
    {
        var response = await _courseApiService.GetAllAsync<CourseDto>();
        if (!response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }
        List<CourseDto> courses = response.Values!;
        return View(courses);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(CourseDto courseDto)
    {
        return View(courseDto);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        return View();
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        var response = await _majorApiService.GetAllAsync<MajorDto>();
        if (!response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        var viewModel = new CourseViewModel
        {
            Majors = response.Values!
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CourseViewModel courseViewModel)
    {
        var response = await _courseApiService.PostAsync(courseViewModel.AddCourseDto);
        if (!response.IsSuccessStatusCode)
        {
            return View();
        }
        return RedirectToAction("Create", "Course");
    }

}
