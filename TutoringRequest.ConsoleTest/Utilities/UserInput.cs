using TutoringRequest.Models.DTO.Auth;

namespace TutoringRequest.ConsoleTest.Utilities;

public class UserInput
{
    public static string MultipleChoice(string prompt, List<Choice> choices, bool printChoices)
    {


        while (true)
        {
            Console.WriteLine(prompt);
            if (printChoices)
            {
                foreach (var choice in choices)
                {
                    Console.WriteLine($"({choice.Name}).{choice.Description}");
                }
            }
            var answer = Console.ReadLine()!;
            Choice? selectedChoice = choices.FirstOrDefault(c => c.Name == answer);
            if (selectedChoice != null)
            {
                selectedChoice.Action();
                return answer;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose a valid option.");
            }
        }
    }
    public static LoginDto AskForLoginInfo()
    {
        Console.WriteLine("Enter your email: ");
        var email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        var password = Console.ReadLine();

        return new LoginDto { Email = email, Password = password };
    }
    public static RegisterDto AskForRegisterInfo()
    {
        Console.WriteLine("Enter your name");
        var name = Console.ReadLine();  
        Console.WriteLine("Enter your email: ");
        var email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        var password = Console.ReadLine();
        Console.WriteLine("Enter your phone number (optional): ");
        var phoneNumber = Console.ReadLine();
        Console.WriteLine("Enter your student number (optional): ");
        var studentNumber = Console.ReadLine();

        return new RegisterDto { Email = email, Password = password, Name = name, PhoneNumber = phoneNumber, StudentNumber = studentNumber};
    }
}
