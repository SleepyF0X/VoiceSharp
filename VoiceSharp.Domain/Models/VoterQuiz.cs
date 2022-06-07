namespace VoiceSharp.Domain.Models;

public class VoterQuiz : Entity
{
    public string VoterEmail { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public string Token { get; set; }
    public bool IsActive { get; set; } = false;
    public string? VoterToken { get; set; }
    public List<VoterAnswer> Answers { get; set; }
}