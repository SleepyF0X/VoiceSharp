using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.ValidationRules.Auth;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules;

public sealed class RefreshTokenRules : IRefreshTokenRules
{
    private readonly IDatabase _context;

    public RefreshTokenRules(IDatabase context)
    {
        _context = context;
    }

    public async Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(_ => _.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
    }
}