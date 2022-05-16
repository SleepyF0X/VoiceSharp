namespace VoiceSharp.Domain.ValidationRules.Auth;

public interface IRegisterRules : IValidationRules
{
    Task<bool> UserNameIsUniqueAsync(string userName, CancellationToken cancellationToken);
}