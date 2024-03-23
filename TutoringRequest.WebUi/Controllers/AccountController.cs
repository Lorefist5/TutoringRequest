using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        if(roles == null || response.Token == null)
        {
            ModelState.AddModelError(string.Empty, "Error occured.");
            return View();
        }
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, loginDto.Email));

        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }


}
