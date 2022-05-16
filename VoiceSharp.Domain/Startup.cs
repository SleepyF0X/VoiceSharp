using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceSharp.Domain.ValidationRules;

namespace VoiceSharp.Domain;

public static class Startup
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.Scan(scanner =>
        {
            scanner
                .FromAssemblyOf<Marker>()
                .AddClasses(classes => classes.AssignableTo(typeof(IValidationRules)))
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime();
        });
        return services;
    }

    private class Marker
    {
    }
}