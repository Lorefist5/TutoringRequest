using System.Net;
using System.Text;
using System.Text.Json;
using TutoringRequest.Models.DTO.Tutors;

namespace TutoringRequest.Services.HttpClientServices.Base;

abstract public class GenericApiService
{
    private HttpClient _httpClient;
    private readonly string _defaultRoute;

    public GenericApiService(HttpClient httpClient, string defaultRoute)
    {
        _httpClient = httpClient;
        this._defaultRoute = defaultRoute;
    }
    public async Task<HttpResponseMessage> PostAsync<T>(T dto) where T : class
    {

        var jsonContent = JsonSerializer.Serialize(dto);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


        // Make a request to the protected endpoint to create a tutor
        var response = await _httpClient.PostAsync(_defaultRoute, content);

        return response;
    }
    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_defaultRoute);

        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        try
        {
            string content = await response.Content.ReadAsStringAsync();
            
            List<T>? items = JsonSerializer.Deserialize<List<T>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true 
            });

            return items ?? new List<T>(); 
        }
        catch (JsonException)
        {

            return new List<T>();
        }
    }
    public async Task<T?> GetAsync<T>(string parameterName, string parameterValue) where T : class
    {
        if (string.IsNullOrEmpty(parameterName) || string.IsNullOrEmpty(parameterValue))
        {
            throw new ArgumentException("Parameter name and value must not be null or empty.");
        }

        // Build the query string with the specified parameter
        string query = $"{_defaultRoute}?{parameterName}={Uri.EscapeDataString(parameterValue)}";

        HttpResponseMessage response = await _httpClient.GetAsync(query);

        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        try
        {
            // Deserialize the response content to T using System.Text.Json
            string content = await response.Content.ReadAsStringAsync();
            T? item = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // This option makes property names case-insensitive
            });

            return item;
        }
        catch (JsonException)
        {
            // Handle JSON parsing error (e.g., invalid JSON)
            throw new InvalidOperationException("Error parsing JSON response.");
        }
    }
    public async Task<HttpResponseMessage> PutAsync<T>(string urlParams, T data) where T : class
    {
        if (string.IsNullOrEmpty(urlParams) || data == null)
        {
            throw new ArgumentException("ID and data must not be null or empty.");
        }

        string apiUrl = $"{_defaultRoute}/{urlParams}";
        string jsonData = JsonSerializer.Serialize(data);
        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);

        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        // Return true if the update was successful
        return response;
    }
    public async Task<HttpResponseMessage> DeleteAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("ID must not be null or empty.");
        }

        string apiUrl = $"{_defaultRoute}/{id}";

        HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        // Return true if the deletion was successful
        return response;
    }

    public GenericApiService AddToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        return this;
    }

}

