namespace VoiceSharp.Domain;

public abstract class Entity : IEntity
{
    public int Id { get; set; }
}

public interface IEntity
{
    public int Id { get; set; }
}