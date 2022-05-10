using VoiceSharp.Framework.EmailService.Models;

namespace VoiceSharp.Framework.EmailService;

public interface IEmailService
{
    public Task SendEmailAsync(Email email, EmailContact contact);
}