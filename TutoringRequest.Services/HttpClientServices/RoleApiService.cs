using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;

public class RoleApiService : GenericApiService
{
    public RoleApiService(HttpClient httpClient, string defaultRoute) : base(httpClient, defaultRoute)
    {
    }



}
