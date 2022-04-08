namespace VoiceSharp.Domain.Contracts;

public interface IWithDateCreated
{
    DateTimeOffset DateCreated { get; }
    void SetDateCreated(DateTimeOffset value);
}