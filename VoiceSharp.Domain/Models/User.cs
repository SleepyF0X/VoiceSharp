using Microsoft.AspNetCore.Identity;

namespace VoiceSharp.Domain.Models;

public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Age { get; set; }
    public string? Address { get; set; }
}