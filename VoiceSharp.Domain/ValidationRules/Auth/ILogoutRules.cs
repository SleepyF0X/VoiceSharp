namespace VoiceSharp.Domain.ValidationRules.Auth;

public interface ILogoutRules : IValidationRules
{
    Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
}