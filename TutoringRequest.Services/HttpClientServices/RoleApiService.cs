using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Role")]
public class RoleApiService : GenericApiService
{
    public RoleApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : base(httpClient, httpContextAccessor)
    {
    }



}
