using Microsoft.AspNetCore.Mvc;

namespace TutoringRequest.WebUi.Controllers;

public class DashBoardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
