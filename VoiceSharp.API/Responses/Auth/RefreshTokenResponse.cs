﻿using System.Collections.Generic;

namespace VoiceSharp.API.Responses.Auth;

public sealed class RefreshTokenResponse
{
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}