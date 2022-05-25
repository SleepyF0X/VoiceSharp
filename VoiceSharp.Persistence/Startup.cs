using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.Persistence;

public static class Startup
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<VoiceSharpContext>(
                options =>
                {
                    options.UseNpgsql(
                        configuration.GetConnectionString(ConnectionStrings.Default)!,
                        x => x.MigrationsAssembly(typeof(Startup).Assembly.GetName().FullName));
                }, ServiceLifetime.Transient)
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<VoiceSharpContext>();
        return services;
    }

    private static class Marker
    {
    }
}