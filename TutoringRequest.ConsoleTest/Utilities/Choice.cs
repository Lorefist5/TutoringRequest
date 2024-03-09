namespace TutoringRequest.ConsoleTest.Utilities;

public class Choice
{

    public Choice(string name, string description, Action action)
    {
        Name = name;
        Description = description;
        Action = action;
    }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Action Action { get; set; }
}
