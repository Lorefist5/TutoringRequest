using Microsoft.AspNetCore.Mvc;

namespace TutoringRequest.WebUi.Controllers;

public class TutorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
