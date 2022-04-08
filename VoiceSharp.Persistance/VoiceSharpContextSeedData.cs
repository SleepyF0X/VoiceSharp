using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.Persistance;

public class VoiceSharpContextSeedData
{
    private const string AdminCredentialsSection = "AdminCredentials";
    private const string AdminUserName = "UserName";
    private const string AdminPassword = "Password";
    private static bool _isInitialized;

    public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration,
        bool isDevelopmentEnvironment)
    {
        if (_isInitialized)
        {
            return;
        }

        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

        if (isDevelopmentEnvironment)
        {
            await SyncDatabaseWithDomainRoles(roleManager);
        }
        else
        {
            await GenerateAndSeedRoles(roleManager);
        }

        await GenerateAndSeedAdmin(userManager, configuration);

        _isInitialized = true;
    }

    private static async Task GenerateAndSeedRoles(RoleManager<IdentityRole<int>> roleManager)
    {
        if (await roleManager.Roles.AnyAsync())
        {
            return;
        }

        foreach (var roleName in Enum.GetNames<DomainRoles>())
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }

    private static async Task SyncDatabaseWithDomainRoles(RoleManager<IdentityRole<int>> roleManager)
    {
        var storedRoles = await roleManager.Roles.Select(_ => _.Name).ToListAsync();
        var domainRoles = Enum.GetNames<DomainRoles>().ToList();
        var union = domainRoles.Union(storedRoles);
        var intersection = domainRoles.Intersect(storedRoles);

        if (!union.Except(intersection).Any())
        {
            return;
        }

        foreach (var roleName in union.Except(intersection))
        {
            if (storedRoles.Any(_ => _.Equals(roleName)))
            {
                var role = await roleManager.FindByNameAsync(roleName);
                await roleManager.DeleteAsync(role);
            }
            else
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }

    private static async Task GenerateAndSeedAdmin(UserManager<User> userManager, IConfiguration configuration)
    {
        var userName = configuration.GetSection(AdminCredentialsSection)[AdminUserName];
        var password = configuration.GetSection(AdminCredentialsSection)[AdminPassword];

        if (await userManager.Users.AnyAsync(_ => _.NormalizedUserName.Equals(userName.ToUpper())))
        {
            return;
        }

        var admin = new User
        {
            Email = userName ?? "admin",
            UserName = userName,
        };

        await userManager.CreateAsync(admin, password ?? "P@ssw0rd!");
        await userManager.AddToRoleAsync(admin, DomainRoles.Admin.ToString());
    }
}