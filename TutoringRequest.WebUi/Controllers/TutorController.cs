using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Models.DTO.Tutors;
using TutoringRequest.Services.HttpClientServices;

namespace TutoringRequest.WebUi.Controllers;

public class TutorController : Controller
{
    private readonly TutorApiService _tutorApiService;

    public TutorController(TutorApiService tutorApiService)
    {
        this._tutorApiService = tutorApiService;
    }
    public async Task<IActionResult> Index()
    {
        var response = await _tutorApiService.GetAllAsync<TutorDto>();
        if (!response.IsSuccess) return RedirectToAction("Index","Home");
        IEnumerable<TutorDto> tutors = response.Values!;

        return View(tutors);
    }


}
