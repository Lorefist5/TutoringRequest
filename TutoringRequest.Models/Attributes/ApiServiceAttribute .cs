namespace TutoringRequest.Models.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ApiServiceAttribute : Attribute
{
    public string Name { get; }

    public ApiServiceAttribute(string name)
    {
        Name = name;
    }
}
