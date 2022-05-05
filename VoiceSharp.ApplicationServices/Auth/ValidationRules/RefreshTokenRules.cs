using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.ValidationRules;
using VoiceSharp.Persistence;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules;

public interface IRefreshTokenRules : IValidationRule
{
    Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
}

public sealed class RefreshTokenRules : IRefreshTokenRules
{
    private readonly VoiceSharpContext _context;

    public RefreshTokenRules(VoiceSharpContext context)
    {
        _context = context;
    }

    public async Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(_ => _.UserName.ToLower().Equals(userName.ToLower()), cancellationToken);
    }
}