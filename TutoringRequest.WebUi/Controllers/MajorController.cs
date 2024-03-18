using Microsoft.AspNetCore.Mvc;

namespace TutoringRequest.WebUi.Controllers;

public class MajorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
