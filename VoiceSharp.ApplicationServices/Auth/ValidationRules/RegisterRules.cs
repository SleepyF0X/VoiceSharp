using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.Models;
using VoiceSharp.Domain.ValidationRules.Auth;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules;

public sealed class RegisterRules : IRegisterRules
{
    private readonly UserManager<User> _userManager;

    public RegisterRules(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> UserNameIsUniqueAsync(string userName, CancellationToken cancellationToken)
    {
        return !await _userManager.Users.AnyAsync(_ => _.NormalizedUserName.Equals(userName.ToUpper()), cancellationToken);
    }
}