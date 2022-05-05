using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceSharp.ApplicationServices.EmailService;
using VoiceSharp.ApplicationServices.Infrastructure.EmailService;
using VoiceSharp.ApplicationServices.JwtAuthService;
using VoiceSharp.Domain.ValidationRules;

namespace VoiceSharp.ApplicationServices
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Scan(scanner =>
            {
                scanner
                    .FromAssemblyOf<Marker>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidationRule)))
                    .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime();
            });
            services.AddJwtAuthService(configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration, string configurationSection)
        {
            services.Configure<EmailServiceSettings>(options => configuration.GetSection(configurationSection).Bind(options));
            services.AddScoped<IEmailService, MailKitEmailService>();

            return services;
        }

        private class Marker
        {
        }
    }
}
