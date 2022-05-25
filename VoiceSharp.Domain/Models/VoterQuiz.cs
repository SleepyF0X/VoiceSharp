namespace VoiceSharp.Domain.Models;

public class VoterQuiz : Entity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public string Token { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
}