using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using VoiceSharp.ApplicationServices.Auth.ValidationRules;
using VoiceSharp.ApplicationServices.JwtAuthService;
using VoiceSharp.ApplicationServices.JwtAuthService.ResultModels;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.ApplicationServices.Auth.Commands;

public sealed class LoginCommand : IRequest<OperationResult<LoginResult>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(ILoginRules rules)
    {
        RuleFor(_ => _.Email)
            .NotEmpty()
            .WithMessage(ErrorConstants.InvalidCredentials)
            .MustAsync(rules.UserIsRegistered)
            .WithMessage(_ => ErrorConstants.InvalidCredentials);

        RuleFor(_ => _.Password)
            .NotEmpty()
            .WithMessage(ErrorConstants.InvalidCredentials);
    }
}

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<LoginResult>>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<OperationResult<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Email);

        var isSucceeded = await _userManager.CheckPasswordAsync(user!, request.Password);

        if (!isSucceeded)
        {
            return OperationResult.Fail<LoginResult>(ErrorConstants.InvalidCredentials);
        }

        var userRoles = await _userManager.GetRolesAsync(user!);
        var roleClaims = userRoles.Select(_ => new Claim(ClaimsIdentity.DefaultRoleClaimType, _));
        var userClaims = new List<Claim>(roleClaims)
        {
            new (ClaimsIdentity.DefaultNameClaimType, user!.UserName),
        };

        var jwtResult = _jwtService.GenerateTokens(user.UserName, userClaims, DateTime.UtcNow);

        return OperationResult.Ok(new LoginResult
        {
            UserName = user.UserName,
            Roles = userRoles.ToList(),
            AccessToken = jwtResult.AccessToken,
            RefreshToken = jwtResult.RefreshToken.TokenString,
        });
    }
}