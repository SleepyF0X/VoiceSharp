namespace VoiceSharp.Domain.ValidationRules.Auth;

public interface ILoginRules : IValidationRules
{
    Task<bool> UserIsRegistered(string userName, CancellationToken cancellationToken);
}