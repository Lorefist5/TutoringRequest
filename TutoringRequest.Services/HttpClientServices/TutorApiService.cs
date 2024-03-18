using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Tutor")]
public class TutorApiService : GenericApiService
{
    public TutorApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
    {
    }
}
