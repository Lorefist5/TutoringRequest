using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TutoringRequest.Api.Services;
using TutoringRequest.Api.Services.Interfaces;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Models.DTO.Tutors;

class Program
{
    static async Task Main()
    {
        await Console.Out.WriteLineAsync("Enter your email: ");
        var email = Console.ReadLine();


        //string apiUrl = "https://localhost:7249/api/";
        //string email = "user1@example.com";
        //string password = "Lorefist";

        //// Make a login request
        //string token = await Login(apiUrl, email, password);

        //if (!string.IsNullOrEmpty(token))
        //{

        //    // Create a tutor with dummy data
        //    var tutorDto = new AddTutorDto
        //    {
        //        Name = "John Doe",
        //        Password = "SecurePassword",
        //        StudentNumber = "123456",
        //        PhoneNumber = "123-456-7890",
        //        Email = "john.doe@example.com"
        //    };

        //    await CreateTutor(apiUrl, token, tutorDto);
        //    await GetTutors(apiUrl, token);
        //}

        //Console.ReadLine(); // Pause console app so you can see the output
    }

    static async Task ResetPassword()
    {
        using (HttpClient client = new HttpClient())
        {

        }
    }
    static async Task<string> Login(string apiUrl, string email, string password)
    {
        using (HttpClient client = new HttpClient())
        {

            var content = new StringContent($"{{\"Email\":\"{email}\", \"Password\":\"{password}\"}}", Encoding.UTF8, "application/json");



            var response = await client.PostAsync($"{apiUrl}Auth/login", content);


            if (!response.IsSuccessStatusCode)
            {
                return "";
            }

            var token = await response.Content.ReadFromJsonAsync<AuthToken>();
            Console.WriteLine($"Token: {token}");

            return token.Token;
        }
    }
    static async Task CreateTutor(string apiUrl, string token, AddTutorDto tutorDto)
    {
        using (HttpClient client = new HttpClient())
        {
            // Attach the JWT token to the Authorization header
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Convert the tutor data to JSON
            var jsonContent = JsonSerializer.Serialize(tutorDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            // Make a request to the protected endpoint to create a tutor
            var response = await client.PostAsync($"{apiUrl}Tutor", content);

            // Ensure the request was successful
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            // Read the response content
            var createdTutor = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Created Tutor: {createdTutor}");
        }
    }

    static async Task GetTutors(string apiUrl, string token)
    {
        using (HttpClient client = new HttpClient())
        {
            // Attach the JWT token to the Authorization header
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // Make a request to the protected endpoint (e.g., GetTutors)
            var response = await client.GetAsync($"{apiUrl}Tutor");

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            // Read the response content
            var tutors = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Tutors: {tutors}");
        }
    }
}