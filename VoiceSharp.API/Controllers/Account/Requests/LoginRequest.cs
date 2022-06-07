using VoiceSharp.ApplicationServices.Auth.Commands;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.Controllers.Account.Requests;

public sealed class LoginRequest : IMapTo<LoginCommand>
{
    public string Email { get; set; }
    public string Password { get; set; }
}