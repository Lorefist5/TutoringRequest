using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Models.DTO.Majors;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.ViewModel;

namespace TutoringRequest.WebUi.Controllers;

public class MajorController : Controller
{
    private readonly MajorApiService _majorApiService;

    public MajorController(MajorApiService majorApiService)
    {
        _majorApiService = majorApiService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _majorApiService.GetAllAsync<MajorDto>();

        if (!response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }
        IEnumerable<MajorDto> majors = response.Values!;
        return View(majors);
    }
    public async Task<IActionResult> Courses(MajorDto majorDto)
    {
        var response = await _majorApiService.GetCoursesByMajor(majorDto.Id);
        if (!response.IsSuccess)
        {
            return RedirectToAction("Index", "Home");
        }

        var courses = response.Values!;
        var majorViewModel = new MajorViewModel(majorDto,courses);
        return View(majorViewModel);
    }
    [Authorize(Roles = "Admin")]
    public  IActionResult Create()
    {
        AddMajorViewModel addMajorViewModel = new AddMajorViewModel();
        return View(addMajorViewModel);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateMethod(AddMajorViewModel addMajorViewModel)
    {
        var response = await _majorApiService.PostAsync(addMajorViewModel.Major);
        if (!response.IsSuccessStatusCode)
        {
            return View();
        }
        TempData["SuccessMessage"] = "Course created successfully!";
        return RedirectToAction("Index","Major");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(MajorDto major)
    {
        return View(major);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMethod(Guid id)
    {
        var response = await _majorApiService.DeleteAsync(id.ToString());
        if (!response.IsSuccessStatusCode)
        {
            return View("Error");
        }
        TempData["SuccessMessage"] = "Mejor deleted successfully!";
        return RedirectToAction("Index", "Major");
    }
}
