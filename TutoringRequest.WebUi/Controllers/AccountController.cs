using Microsoft.AspNetCore.Mvc;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Services.HttpClientServices;

namespace TutoringRequest.WebUi.Controllers;

public class AccountController : Controller
{
    private readonly AuthApiService _authApiService;

    public AccountController(AuthApiService authApiService)
    {
        this._authApiService = authApiService;
    }
    public IActionResult Login()
    {
        CheckAndAddTokenToService();
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var logingDto = new LoginDto() { Email = email, Password = password };
        var response = await _authApiService.LoginAsync(logingDto);

        if (!response.IsSuccessful) return View();
        Response.Cookies.Append("LoginCookie", response.Token!.Token);
        _authApiService.AddToken(response.Token!.Token);
        return RedirectToAction("Index", "Home");
    }


    private void CheckAndAddTokenToService()
    {
        string? loginCookie = HttpContext.Request.Cookies["LoginCookie"];

        if (!string.IsNullOrWhiteSpace(loginCookie))
        {
            _authApiService.AddToken(loginCookie);
        }
    }
}
