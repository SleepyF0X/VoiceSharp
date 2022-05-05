using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.ValidationRules;
using VoiceSharp.Persistence;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules;

public interface ILogoutRules : IValidationRule
{
    Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
}

public sealed class LogoutRules : ILogoutRules
{
    private readonly VoiceSharpContext _context;

    public LogoutRules(VoiceSharpContext context)
    {
        _context = context;
    }

    public async Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(_ => _.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
    }
}