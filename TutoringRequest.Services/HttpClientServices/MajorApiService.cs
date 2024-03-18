using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Major")]
public class MajorApiService : GenericApiService
{
    public MajorApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string? defaultRoute = null) : base(httpClient, httpContextAccessor, defaultRoute)
    {
    }
}
