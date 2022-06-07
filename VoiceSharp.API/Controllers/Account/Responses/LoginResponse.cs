using System.Collections.Generic;
using VoiceSharp.ApplicationServices.JwtAuthService.ResultModels;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.Controllers.Account.Responses;

public class LoginResponse : IMapFrom<LoginResult>
{
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}