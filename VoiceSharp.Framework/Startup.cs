using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceSharp.ApplicationServices.Infrastructure.EmailService;
using VoiceSharp.Framework.EmailService;

namespace VoiceSharp.Framework;

public static class Startup
{
    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration, string configurationSection)
    {
        services.Configure<EmailServiceSettings>(options => configuration.GetSection(configurationSection).Bind(options));
        services.AddScoped<IEmailService, MailKitEmailService>();

        return services;
    }
}