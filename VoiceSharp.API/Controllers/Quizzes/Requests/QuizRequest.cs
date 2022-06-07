using System.Collections.Generic;
using AutoMapper;
using VoiceSharp.ApplicationServices.Quiz.Commands;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.Contracts.AutoMapper;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.API.Controllers.Quizzes.Requests;

public class QuizRequestModel : IHaveCustomMapping
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<QuestionRequestModel> Questions { get; set; }

    public void CreateMappings(Profile configuration)
    {
        configuration.CreateMap<QuizRequestModel, QuizDto>();
        configuration.CreateMap<QuizRequestModel, CreateQuizCommand>()
            .ForMember(d => d.Quiz, opt => opt.MapFrom(s => s));
    }
}

public class QuestionRequestModel : IMapTo<QuestionDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public QuestionType Type  { get; set; }
    public List<AnswerRequestModel> Answers { get; set; }
}

public class AnswerRequestModel : IMapTo<AnswerDto>
{
    public string Text { get; set; }
    public string Value { get; set; }
}