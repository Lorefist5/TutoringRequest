using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Course")]
public class CourseApiService : GenericApiService
{
    public CourseApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string? defaultRoute = null) : base(httpClient, httpContextAccessor, defaultRoute)
    {
    }
}
