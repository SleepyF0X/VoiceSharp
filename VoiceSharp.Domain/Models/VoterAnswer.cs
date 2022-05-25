namespace VoiceSharp.Domain.Models;

public class VoterAnswer : Entity 
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
}