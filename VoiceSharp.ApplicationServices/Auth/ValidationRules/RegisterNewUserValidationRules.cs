using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.Models;
using VoiceSharp.Domain.ValidationRules;

namespace VoiceSharp.ApplicationServices.Auth.ValidationRules
{
    public interface IRegisterNewUserValidationRules : IValidationRule
    {
        Task<bool> UserNameIsUnique(string userName, CancellationToken cancellationToken);
    }

    public sealed class RegisterNewUserValidationRules : IRegisterNewUserValidationRules
    {
        private readonly UserManager<User> _userManager;

        public RegisterNewUserValidationRules(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UserNameIsUnique(string userName, CancellationToken cancellationToken)
        {
            return !await _userManager.Users.AnyAsync(_ => _.NormalizedUserName.Equals(userName.ToUpper()), cancellationToken);
        }
    }
}
