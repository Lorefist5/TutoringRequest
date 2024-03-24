using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutoringRequest.Models.DTO.Accounts;
using TutoringRequest.Services.HttpClientServices;
using TutoringRequest.WebUi.ViewModel.Change;
using TutoringRequest.WebUi.Services;
using System.IdentityModel.Tokens.Jwt;

namespace TutoringRequest.WebUi.Controllers;
[Authorize]
public class ChangeController : Controller
{
    private readonly AccountApiService _accountApiService;
    private readonly UserAuthenticationService _userAuthenticationService;

    public ChangeController(AccountApiService accountApiService, UserAuthenticationService userAuthenticationService)
    {
        this._accountApiService = accountApiService;
        this._userAuthenticationService = userAuthenticationService;
    }
    public IActionResult Email()
    {
        var currentEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (currentEmail == null)
        {
            return RedirectToAction("Login","Account");
        }


        return View(new ChangeEmailViewModel() { CurrentEmail = currentEmail});
    }
    public IActionResult Password()
    {
        return View();
    }
    public async Task<IActionResult> Phone()
    {
        var currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid parsedId;
        if (string.IsNullOrEmpty(currentId) || !Guid.TryParse(currentId, out parsedId))
        {
            return RedirectToAction("Login", "Account");
        }
        var accountInfo = await _accountApiService.GetAccountInfoAsync(parsedId);
        if(!accountInfo.IsSuccess) return RedirectToAction("Index", "Home");
        return View(new ChangePhoneViewModel() { PhoneOldNumber = accountInfo.Value!.PhoneNumber});
    }
    public IActionResult Username()
    {
        var currentUserName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (currentUserName == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(new ChangeUsernameViewModel() { CurrentUsername = currentUserName});
    }

    //Methods
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel changeEmailViewModel)
    {
        if (changeEmailViewModel.NewEmail != changeEmailViewModel.ConfirmNewEmail)
        {
            ModelState.AddModelError(string.Empty, "The new email and confirmation email do not match.");
            return RedirectToAction("Email", "Change", changeEmailViewModel);
        }

        ChangeEmailDto changeEmailDto = new ChangeEmailDto
        {
            NewEmail = changeEmailViewModel.NewEmail,
            OldEmail = changeEmailViewModel.CurrentEmail
        };

        
        var response = await _accountApiService.ChangeEmailAsync(changeEmailDto);

        
        if (!response.IsSuccessful)
        {
            
            ModelState.AddModelError(string.Empty, "Failed to change email.");
            return RedirectToAction("Email", "Change", changeEmailViewModel);
        }

        
        var newToken = response.Token!.Token;
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        Response.Cookies.Delete("LoginCookie");
        Response.Cookies.Append("LoginCookie", newToken, new CookieOptions { HttpOnly = true, Secure = HttpContext.Request.IsHttps });

        var signInResult = await _userAuthenticationService.SignInUserAsync(response.Token.Token);



        if (!signInResult)
        {
            ModelState.AddModelError(string.Empty, "Failed to sign in.");
            return RedirectToAction("Email", "Change", changeEmailViewModel);
        }

        return RedirectToAction("Settings", "Account");
    }
    [HttpPost]
    public async Task<IActionResult> ChangeUserName(ChangeUsernameViewModel changeUsernameViewModel)
    {
        if (changeUsernameViewModel.CurrentUsername == changeUsernameViewModel.NewUsername)
        {
            ModelState.AddModelError(string.Empty, "The old username can't be the same as the new one");
            return RedirectToAction("Username", "Change", changeUsernameViewModel);
        }


        var currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid parsedId;
        if (string.IsNullOrEmpty(currentId) || !Guid.TryParse(currentId, out parsedId))
        {
            return RedirectToAction("Login", "Account");
        }

        var response = await _accountApiService.ChangeUserNameAsync(parsedId, changeUsernameViewModel.NewUsername);


        if (!response.IsSuccessful)
        {

            ModelState.AddModelError(string.Empty, "Failed to change username.");
            return RedirectToAction("Username", "Change", changeUsernameViewModel);
        }


        var newToken = response.Token!.Token;
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        Response.Cookies.Delete("LoginCookie");
        Response.Cookies.Append("LoginCookie", newToken, new CookieOptions { HttpOnly = true, Secure = HttpContext.Request.IsHttps });

        var signInResult = await _userAuthenticationService.SignInUserAsync(response.Token.Token);



        if (!signInResult)
        {
            ModelState.AddModelError(string.Empty, "Failed to sign in.");
            return RedirectToAction("Username", "Change", changeUsernameViewModel);
        }

        return RedirectToAction("Settings", "Account");
    }
    [HttpPost]
    public async Task<IActionResult> ChangePhone(ChangePhoneViewModel changePhoneViewModel)
    {
        if (changePhoneViewModel.PhoneOldNumber == changePhoneViewModel.PhoneNewNumber)
        {
            ModelState.AddModelError(string.Empty, "The old phone number can't be the same as the new one");
            return RedirectToAction("Phone", "Change", changePhoneViewModel);
        }


        var currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid parsedId;
        if (string.IsNullOrEmpty(currentId) || !Guid.TryParse(currentId, out parsedId))
        {
            return RedirectToAction("Phone", "Change", changePhoneViewModel);
        }

        var response = await _accountApiService.ChangePhoneAsync(parsedId, changePhoneViewModel.PhoneNewNumber);


        if (!response.IsSuccessful)
        {

            ModelState.AddModelError(string.Empty, "Failed to change username.");
            return RedirectToAction("Phone", "Change", changePhoneViewModel);
        }


        var newToken = response.Token!.Token;
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        Response.Cookies.Delete("LoginCookie");
        Response.Cookies.Append("LoginCookie", newToken, new CookieOptions { HttpOnly = true, Secure = HttpContext.Request.IsHttps });

        var signInResult = await _userAuthenticationService.SignInUserAsync(response.Token.Token);



        if (!signInResult)
        {
            ModelState.AddModelError(string.Empty, "Failed to sign in.");
            return RedirectToAction("Phone", "Change", changePhoneViewModel);
        }

        return RedirectToAction("Settings", "Account");
    }
    public async Task<IActionResult>ChangePassword(ChangePasswordViewModel changePasswordViewModel)
    {
        var currentId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid parsedId;
        if (string.IsNullOrEmpty(currentId) || !Guid.TryParse(currentId, out parsedId))
        {
            return RedirectToAction("Login", "Account");
        }
        if (changePasswordViewModel == null)
        {
            ModelState.AddModelError(string.Empty, "The password can't be empty");
            return RedirectToAction("Password", "Change", changePasswordViewModel);
        }
        if(changePasswordViewModel.OldPassword == changePasswordViewModel.NewPassword)
        {
            ModelState.AddModelError(string.Empty, "Old password and the  new password can't be the same");
            return RedirectToAction("Password", "Change", changePasswordViewModel);
        }
        if(changePasswordViewModel.NewPassword != changePasswordViewModel.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords don't match");
            return RedirectToAction("Password", "Change", changePasswordViewModel);
        }
        if (string.IsNullOrWhiteSpace(changePasswordViewModel.NewPassword))
        {
            ModelState.AddModelError(string.Empty, "Password can't be empty");
            return RedirectToAction("Password", "Change", changePasswordViewModel);
        }

        var response = await _accountApiService.ChangePasswordAsync(parsedId, new ChangePasswordDto() { NewPassword = changePasswordViewModel.NewPassword, OldPassword = changePasswordViewModel.OldPassword});
        if (!response.IsSuccessful)
        {
            ModelState.AddModelError(string.Empty, response.Message);
            return RedirectToAction("Password", "Change", changePasswordViewModel);
        }
        
        return RedirectToAction("Settings", "Account");
    }
}
