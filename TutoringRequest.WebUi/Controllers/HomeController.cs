using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TutoringRequest.Models.DTO.Tutors;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.Models;

namespace TutoringRequest.WebUi.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TutorApiService _tutorApiService;

    public HomeController(ILogger<HomeController> logger, TutorApiService tutorApiService)
    {
        _logger = logger;
        this._tutorApiService = tutorApiService;
    }
    [Authorize]
    public async Task<IActionResult> Index()
    {
        return View();
    }
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
