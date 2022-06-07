using VoiceSharp.ApplicationServices.Auth.Commands;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.Controllers.Account.Requests;

public sealed class RegisterNewUserRequest : IMapTo<RegisterNewUserCommand>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}