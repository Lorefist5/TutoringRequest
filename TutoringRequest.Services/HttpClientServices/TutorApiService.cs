using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;

public class TutorApiService : GenericApiService
{
    public TutorApiService(HttpClient httpClient, string defaultRoute) : base(httpClient, defaultRoute)
    {
    }
}
