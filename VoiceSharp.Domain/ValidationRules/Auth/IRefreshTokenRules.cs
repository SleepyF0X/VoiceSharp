namespace VoiceSharp.Domain.ValidationRules.Auth;

public interface IRefreshTokenRules : IValidationRules
{
    Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
}