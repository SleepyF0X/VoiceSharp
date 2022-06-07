using VoiceSharp.Domain.Contracts;

namespace VoiceSharp.Domain.Models;

public class Quiz : Entity, IWithDateCreated
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<VoterQuiz> Voters { get; set; }
    public List<Question> Questions { get; set; }
    public DateTimeOffset DateCreated { get; private set; }
    void IWithDateCreated.SetDateCreated(DateTimeOffset value) => DateCreated = value;
}