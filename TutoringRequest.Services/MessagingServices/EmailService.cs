using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using TutoringRequest.Models.Domain;
using TutoringRequest.Services.Interfaces;

namespace TutoringRequest.Services.MessagingServices;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mail = _configuration["EmailSettings:Email"];
        var password = _configuration["EmailSettings:Password"];

        using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
        {
            try
            {
                smtpClient.Credentials = new NetworkCredential(mail, password);
                smtpClient.EnableSsl = true;
                

                var mailMessage = new MailMessage(from: mail, to: to, subject, body);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }


    public async Task SendResetEmail(string to, ResetToken resetToken)
    {
        try
        {
            var mail = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential(mail, password);
                smtpClient.EnableSsl = true;

                var resetLink = $"https://localhost:7249/api/Auth/resetpassword?resetTokenId={resetToken.Id}";

                // Include the link in the email body as a clickable hyperlink
                var body = $"Click <a href=\"{resetLink}\">here</a> to reset your password.";

                var subject = "Password Reset Request";
                var mailMessage = new MailMessage(from: mail, to: to, subject, body);

                // Set the message body format to HTML
                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending reset email: {ex.Message}");
        }
    }




}
