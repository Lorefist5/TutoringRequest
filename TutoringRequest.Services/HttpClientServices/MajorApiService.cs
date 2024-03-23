using Microsoft.AspNetCore.Http;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Models.DTO.Courses;
using TutoringRequest.Models.DTO.Http;
using TutoringRequest.Services.HttpClientServices.Base;

namespace TutoringRequest.Services.HttpClientServices;
[ApiService("Major")]
public class MajorApiService : GenericApiService
{
    public MajorApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string? defaultRoute = null) : base(httpClient, httpContextAccessor, defaultRoute)
    {
    }
    public async Task<HttpResponse<CourseDto>> GetCoursesByMajor(Guid majorId)
    {
        string url = $"{_defaultRoute}/{majorId}/courses";

        var courses = await GetAsync<List<CourseDto>>(url);
        var response = new HttpResponse<CourseDto>()
        {
            Values = courses ?? new List<CourseDto>(),
            IsSuccess = true
        };
        return response;
    }

}
