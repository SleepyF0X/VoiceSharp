using VoiceSharp.Domain.Contracts;

namespace VoiceSharp.Domain.Models;

public class Vote : Entity, IWithDateCreated
{
    public string Name { get; set; }
    public DateTimeOffset DateCreated { get; private set; }
    void IWithDateCreated.SetDateCreated(DateTimeOffset value) => DateCreated = value;
}