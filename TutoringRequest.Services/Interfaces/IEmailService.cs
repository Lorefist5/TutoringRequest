using TutoringRequest.Models.Domain;

namespace TutoringRequest.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);

    Task SendResetEmail(string to, ResetToken resetToken);
}
