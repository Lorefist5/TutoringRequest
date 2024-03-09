using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TutoringRequest.Api.Helpers.ApiHelpers;

public class SectionHelper
{

    public static Guid? GetCurrentUserId(ControllerBase controllerBase)
    {
        string userIdString = controllerBase.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value; // Will always be != null because you need to be admin role to access the API

        // Check if the string is a valid GUID
        if (Guid.TryParse(userIdString, out Guid userId))
        {
            return userId;
        }
        else
        {
            return null;
        }
    }
}
