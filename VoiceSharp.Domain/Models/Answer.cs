namespace VoiceSharp.Domain.Models;

public class Answer : Entity
{
    public string Text { get; set; }
    public string Value { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}