using VoiceSharp.ApplicationServices.Auth.Commands;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.Requests.Auth;

public sealed class RefreshTokenRequest : IMapTo<RefreshTokenCommand>
{
    public string RefreshToken { get; set; }
}