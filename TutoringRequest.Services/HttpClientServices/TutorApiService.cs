using TutoringRequest.Models.Attributes;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Tutor")]
public class TutorApiService : GenericApiService
{
    public TutorApiService(HttpClient httpClient) : base(httpClient)
    {
    }
}
