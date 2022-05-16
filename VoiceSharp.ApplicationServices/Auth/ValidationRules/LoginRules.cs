using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.Models;
using VoiceSharp.Domain.ValidationRules.Auth;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules;

public sealed class LoginRules : ILoginRules
{
    private readonly UserManager<User> _userManager;

    public LoginRules(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> UserIsRegistered(string? userName, CancellationToken cancellationToken)
    {
        if (userName == null)
        {
            return false;
        }
        
        return await _userManager.Users.AnyAsync(_ => _.NormalizedUserName == userName.ToUpper(), cancellationToken);
    }
}