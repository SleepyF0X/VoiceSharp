using VoiceSharp.Domain.Contracts.AutoMapper;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.ApplicationServices.Quiz.Dtos;

public class QuizDto : IMapTo<Domain.Models.Quiz>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<QuestionDto> Questions { get; set; }
}

public class QuestionDto : IMapTo<Question>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public QuestionType Type  { get; set; }
    public List<AnswerDto> Answers { get; set; }
}

public class AnswerDto : IMapTo<Answer>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Value { get; set; }
}