using System.Collections.Generic;
using VoiceSharp.ApplicationServices.Quiz.Commands;
using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.API.Controllers.Quizzes.Requests;

public class SubmitVoteRequest : IMapTo<SubmitVoteCommand>
{
    public string Token { get; set; }
    public List<QuestionAnswerRequest> Answers { get; set; }
    public string Password { get; set; }
    public string Identificator { get; set; }
}

public class QuestionAnswerRequest : IMapTo<SubmitVoteCommand.QuestionAnswer>
{
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
}