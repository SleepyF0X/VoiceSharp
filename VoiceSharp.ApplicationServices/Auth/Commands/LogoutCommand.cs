using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VoiceSharp.ApplicationServices.Auth.ValidationRules;
using VoiceSharp.ApplicationServices.JwtAuthService;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.ApplicationServices.Auth.Commands
{
    public sealed class LogoutCommand : IRequest<OperationResult>
    {
        public string UserName { get; set; }
    }

    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator(ILogoutRules rules)
        {
            RuleFor(_ => _.UserName)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(User.UserName)))
                .MustAsync(rules.UserIsRegistered)
                .WithMessage(_ => string.Format(ErrorConstants.EntityNotFound, "User", nameof(User.Email), _.UserName));
        }
    }

    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, OperationResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;

        public LogoutCommandHandler(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<OperationResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            _jwtService.RemoveRefreshTokenByUserName(user.UserName);

            return OperationResult.Ok();
        }
    }
}
