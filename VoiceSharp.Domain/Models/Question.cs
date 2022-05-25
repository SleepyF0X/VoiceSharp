namespace VoiceSharp.Domain.Models;

public class Question : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public QuestionType Type  { get; set; }
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public List<Answer> Answers { get; set; }
}

public enum QuestionType
{
    Select = 1,
    Checkbox = 2,
    Text = 3,
}