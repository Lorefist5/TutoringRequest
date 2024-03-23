using Microsoft.AspNetCore.Mvc;

namespace TutoringRequest.WebUi.Controllers;

public class StudentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
