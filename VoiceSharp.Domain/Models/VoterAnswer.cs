namespace VoiceSharp.Domain.Models;

public class VoterAnswer : Entity 
{
    public int VoterQuizId { get; set; }
    public VoterQuiz VoterQuiz { get; set; }
    public int QuestionId { get; set; }
    public Question Question { get; set; }
    public int AnswerId { get; set; }
    public Answer Answer { get; set; }
}