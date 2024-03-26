using Microsoft.AspNetCore.Mvc;

namespace TutoringRequest.WebUi.Controllers;

public class StudentController : Controller
{
    public StudentController()
    {
        
    }
    public IActionResult Index()
    {

        return View();
    }
}
