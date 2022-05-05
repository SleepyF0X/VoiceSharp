using VoiceSharp.ApplicationServices.EmailService.Models;

namespace VoiceSharp.ApplicationServices.EmailService;

public interface IEmailService
{
    public Task SendEmailAsync(Email email, EmailContact contact);
}