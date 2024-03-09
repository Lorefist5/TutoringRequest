using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TutoringRequest.ConsoleTest.Utilities;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Auth;
using TutoringRequest.Models.DTO.Roles;
using TutoringRequest.Models.DTO.Tutors;
using TutoringRequest.Services.HttpClientServices;

class Program
{
    static async Task Main()
    {
        var apiUrl = "https://localhost:7249/api/";
        HttpClient client = new HttpClient();
        AuthApiService authApiService = new AuthApiService(client);
        TutorApiService tutorApiService = new TutorApiService(client, "Tutor");
        RoleApiService roleApiService = new RoleApiService(client, "Role");
        client.BaseAddress = new Uri(apiUrl);
        bool isAuthorized = false;
        var choices = new List<Choice>
        {
            new Choice("L", "Log in into the application", async () =>
            {
                var loginInfo = UserInput.AskForLoginInfo();
                var response = await authApiService.LoginAsync(loginInfo);
                isAuthorized = response.IsSuccessful;
                if (!response.IsSuccessful)
                {
                    await Console.Out.WriteLineAsync(response.Message);
                    return;
                }
                await Console.Out.WriteLineAsync("Log in successful!");
                AuthorizedApis(authApiService.Roles!);
            }),
            new Choice("R", "Register into the application", async () =>
            {
                var loginInfo = UserInput.AskForRegisterInfo();
                var response = await authApiService.RegisterAsync(loginInfo);
                isAuthorized = response.IsSuccessful;
                if (!response.IsSuccessful)
                {
                    await Console.Out.WriteLineAsync(response.Message);
                    return;
                }
                await Console.Out.WriteLineAsync("Log in successful!");
                AuthorizedApis(authApiService.Roles!);
            }),
            new Choice("F", "Forgot password", () =>
            {

            }),
            new Choice("R", "Reset password", () =>
            {

            })
        };
        while (!isAuthorized)
        {
            UserInput.MultipleChoice("Welcome to Tutoring request!", choices, true);
        }



        Console.ReadLine();
    }




    private static void AuthorizedApis(List<string> roles)
    {
        var defaultApis = new List<Choice>()
        {
            new Choice("T", "Tutoring apis section", () =>
            {

            }),
            new Choice("S", "Student apis section", () =>
            {

            }),

        };
        var adminApis = new List<Choice>()
        {
            new Choice("R", "Role apis section", () =>
            {

            })
        };
        var allApis = new List<Choice>(defaultApis);
        if (roles.Contains("Admin")) allApis.AddRange(adminApis);

        UserInput.MultipleChoice("Select the api section", allApis,true);
    }
}