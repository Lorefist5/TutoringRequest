using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using TutoringRequest.Models.Attributes;
using TutoringRequest.Models.DTO.Http;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Services.HttpClientServices.Base;

public abstract class GenericApiService
{
    protected HttpClient _httpClient;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly string _defaultRoute;

    public GenericApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, string? defaultRoute = null)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _defaultRoute = defaultRoute ?? GetDefaultRoute();
    }


    protected HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri, HttpContent? content = null)
    {
        var message = new HttpRequestMessage(method, requestUri);
        if (content != null)
        {
            message.Content = content;
        }

        var token = _httpContextAccessor.HttpContext?.Request.Cookies["LoginCookie"];
        if (!string.IsNullOrWhiteSpace(token))
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return message;
    }

    public async Task<HttpResponseMessage> PostAsync<T>(T dto) where T : class
    {
        var jsonContent = JsonSerializer.Serialize(dto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var requestMessage = CreateRequestMessage(HttpMethod.Post, _defaultRoute, content);

        return await _httpClient.SendAsync(requestMessage);
    }

    public async Task<HttpResponse<T>> GetAllAsync<T>() where T : class
    {
        var requestMessage = CreateRequestMessage(HttpMethod.Get, _defaultRoute);
        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

        HttpResponse<T> httpResponse = new HttpResponse<T>
        {
            IsSuccess = response.IsSuccessStatusCode,
        };

        if (response.IsSuccessStatusCode)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();
                List<T>? items = JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                httpResponse.Values = items ?? new List<T>();
                return httpResponse;
            }
            catch (JsonException jsonException)
            {
                // Handle or log the exception as needed
                return httpResponse;
            }
        }
        return httpResponse;
    }
    public async Task<T?> GetAsync<T>(string url) where T : class
    {
        var requestMessage = CreateRequestMessage(HttpMethod.Get, url);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        try
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("Error parsing JSON response.");
        }
    }

    public async Task<T?> GetAsync<T>(string parameterName, string parameterValue) where T : class
    {
        string query = $"{_defaultRoute}?{parameterName}={Uri.EscapeDataString(parameterValue)}";
        var requestMessage = CreateRequestMessage(HttpMethod.Get, query);

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        try
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("Error parsing JSON response.");
        }
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string urlParams, T data) where T : class
    {
        string apiUrl = $"{_defaultRoute}/{urlParams}";
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var requestMessage = CreateRequestMessage(HttpMethod.Put, apiUrl, content);

        return await _httpClient.SendAsync(requestMessage);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        string apiUrl = $"{_defaultRoute}/{id}";
        var requestMessage = CreateRequestMessage(HttpMethod.Delete, apiUrl);

        return await _httpClient.SendAsync(requestMessage);
    }

    private string GetDefaultRoute()
    {
        var attribute = GetType().GetCustomAttribute<ApiServiceAttribute>();

        if (attribute != null)
        {
            return attribute.Name;
        }

        string className = GetType().Name;
        if (className.EndsWith("ApiService"))
        {
            className = className.Substring(0, className.Length - "ApiService".Length);
        }

        return className;
    }
}


