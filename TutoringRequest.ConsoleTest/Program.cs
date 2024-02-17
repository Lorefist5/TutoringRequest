using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TutoringRequest.ConsoleTest.Utilities;
using TutoringRequest.Models.Domain;
using TutoringRequest.Models.DTO.Auth;
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
        client.BaseAddress = new Uri(apiUrl);
        var choices = new Dictionary<string, string>()
        {
            { "L" ,"Login to the app" },
            { "S", "Sign up to the app"},
            { "F", "Forgot password"},
            { "R", "Reset password"},
        };
        var token = "";

        while (string.IsNullOrWhiteSpace(token))
        {
            var answer = UserInput.MultipleChoice("Select the api", choices, true);
            if (answer == "L")
            {
                var loginDto = UserInput.AskForLoginInfo();
                token = await authApiService.LoginAsync(loginDto);
                await Console.Out.WriteLineAsync(token);
            }
            else if (answer == "S")
            {
                var registerDto = UserInput.AskForRegisterInfo();
                token = await authApiService.RegisterAsync(registerDto);
                await Console.Out.WriteLineAsync(token);
            }
            else if (answer == "F")
            {
                await Console.Out.WriteLineAsync("Enter your email: ");
                var resetPasswordEmail = Console.ReadLine();

                var isSuccessfullForgotPassword = await authApiService.ForgotPassword(resetPasswordEmail);
                if(isSuccessfullForgotPassword) await Console.Out.WriteLineAsync($"Email was sent to {resetPasswordEmail}");
            }
            else if(answer == "R")
            {
                await Console.Out.WriteLineAsync("Enter your reset token: ");
                var resetTokenString = Console.ReadLine();
                if (Guid.TryParse(resetTokenString, out Guid resetToken))
                {
                    await Console.Out.WriteLineAsync("Enter your new password: ");
                    var newPassword = Console.ReadLine();
                    var isPasswordResetSuccessful = await authApiService.ResetPassword(resetToken, newPassword);
                    if (isPasswordResetSuccessful) await Console.Out.WriteLineAsync("Password reset was sucessful");
                    else await Console.Out.WriteLineAsync("Password reset failed");
                }
                else
                {
                    Console.WriteLine("Invalid GUID string");
                }

            }
        }

        if (!string.IsNullOrEmpty(token))
        {
            await Console.Out.WriteLineAsync("Access granted.");
            var authorizedChoices = new Dictionary<string, string>()
            {
                { "T" ,"Tutor section" },
                { "S", "Student section"},
            };

            var authorizedAnswer = UserInput.MultipleChoice("Select the authorized apis", authorizedChoices, true);

            if(authorizedAnswer == "T")
            {
                var tutorChoices = new Dictionary<string, string>()
                {
                    { "G" , "Get all the tutors" },
                    { "U" , "Update a tutor" },
                    { "C" , "Create a tutor" },
                    { "D" , "Delete a tutor" },
                    { "G1" , "Get a tutor" },
                };
                var tutorAnswer = UserInput.MultipleChoice("Select a tutor api", tutorChoices, true);
                tutorApiService.AddToken(token);

                if(tutorAnswer  == "G")
                {
                    var tutors = await tutorApiService.GetAllAsync<TutorDto>();

                    foreach(TutorDto tutor in tutors)
                    {
                        await Console.Out.WriteLineAsync(tutor.Name);
                    }
                }
                else if(tutorAnswer == "U")
                {

                }
                else if (tutorAnswer == "C")
                {
                    RegisterDto registerUser = UserInput.AskForRegisterInfo();
                    AddTutorDto addTutorDto = new AddTutorDto()
                    {
                        Email = registerUser.Email,
                        Name = registerUser.Name,
                        Password = registerUser.Password,
                        PhoneNumber = registerUser.PhoneNumber,
                        StudentNumber = registerUser.StudentNumber
                    };
                    bool isSuccess = await tutorApiService.PostAsync<AddTutorDto>(addTutorDto);
                    if (isSuccess) await Console.Out.WriteLineAsync("Tutor added");
                    else await Console.Out.WriteLineAsync("Tutor wasn't added");
                }
                else if (tutorAnswer == "D")
                {

                }
                else if (tutorAnswer == "")
                {

                }
            }
        }
        Console.ReadLine();
    }


}