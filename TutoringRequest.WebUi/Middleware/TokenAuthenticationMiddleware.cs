using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using TutoringRequest.Services.HttpClientServices;

namespace TutoringRequest.WebUi.Middleware;

//public class TokenAuthenticationMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TokenAuthenticationMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context, AuthApiService authApiService)
//    {
//        var tokenCookie = context.Request.Cookies["LoginCookie"];

//        if (!string.IsNullOrWhiteSpace(tokenCookie))
//        {
//            // Check if the token is valid and not expired
//            if (!IsTokenExpired(tokenCookie))
//            {
//                // Authenticate the user with the token
                

//                if (response.IsSuccessful)
//                {
//                    // Authentication succeeded, set the user principal
//                    var identity = new ClaimsIdentity(response.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                    var principal = new ClaimsPrincipal(identity);

//                    context.User = principal;
//                }
//                else
//                {
//                    // Token validation failed, remove the invalid token
//                    context.Response.Cookies.Delete("LoginCookie");
//                }
//            }
//            else
//            {
//                // Token is expired, remove it
//                context.Response.Cookies.Delete("LoginCookie");
//            }
//        }

//        // Continue processing the request
//        await _next(context);
//    }

//    private bool IsTokenExpired(string token)
//    {
//        // Implement your token expiration check logic here
//        // This can be similar to the IsTokenExpired method in your controller
//        // ...

//        return false; // Replace with your actual check
//    }
//}

//public static class TokenAuthenticationMiddlewareExtensions
//{
//    public static IApplicationBuilder UseTokenAuthentication(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<TokenAuthenticationMiddleware>();
//    }
//}

