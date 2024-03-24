using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.Services;

namespace TutoringRequest.WebUi.Controllers;

public class AccountController : Controller
{
    private readonly AuthApiService _authApiService;
    private readonly UserAuthenticationService _userAuthenticationService;

    public AccountController(AuthApiService authApiService, UserAuthenticationService userAuthenticationService)
    {
        this._authApiService = authApiService;
        this._userAuthenticationService = userAuthenticationService;
    }
    [AllowAnonymous]
    public IActionResult Login()
    {

        return View();
    }
    [AllowAnonymous]
    public IActionResult Register()
    {
        
        return View();
    }
    [Authorize]
    public IActionResult Settings()
    {
        return View();
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var response = await _authApiService.LoginAsync(loginDto);
        if (!response.IsSuccessful)
        {
            ModelState.AddModelError(string.Empty, response.Message);
            return View();
        }
           
        var roles = response.Roles;

        if (roles == null || response.Token == null)
        {
            ModelState.AddModelError(string.Empty, "Error occured.");
            return View();
        }

        await _userAuthenticationService.SignInUserAsync(response.Token!.Token);
        Response.Cookies.Append("LoginCookie", response.Token.Token, new CookieOptions { HttpOnly = true });

        return RedirectToAction("Index", "Home");

    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var response = await _authApiService.RegisterAsync(registerDto);
        if (!response.IsSuccessful)
        {
            ModelState.AddModelError(string.Empty, response.Message);
            return View();
        }
        Response.Cookies.Append("LoginCookie", response.Token!.Token);
        _authApiService.AddToken(response.Token!.Token);

        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }


}
