using FluentValidation;
using Flurl;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VoiceSharp.ApplicationServices.Hash;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.Models;
using VoiceSharp.Domain.ValidationRules.Auth;
using VoiceSharp.Framework.EmailService;
using VoiceSharp.Framework.EmailService.Constants;
using VoiceSharp.Framework.EmailService.Models;

namespace VoiceSharp.ApplicationServices.Quiz.Commands;


    public class SubmitVoteCommand : IRequest<OperationResult>
    {
        public string Token { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
        public string Password { get; set; }
        public string Identificator { get; set; }
        public class QuestionAnswer
        {
            public int QuestionId { get; set; }
            public int AnswerId { get; set; }
        }
    }

    public class SubmitVoteCommandValidator : AbstractValidator<SubmitVoteCommand>
    {
        public SubmitVoteCommandValidator(IRegisterRules rules)
        {
            /*RuleFor(_ => _.Quiz.Name)
                .NotEmpty()
                .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(SubmitVoteCommand.Quiz.Name)));*/
        }
    }

    public sealed class SubmitVoteCommandHandler : IRequestHandler<SubmitVoteCommand, OperationResult>
    {
        private readonly IDatabase _context;
        private readonly IEmailService _emailService;


        public SubmitVoteCommandHandler(IDatabase context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<OperationResult> Handle(SubmitVoteCommand request, CancellationToken cancellationToken)
        {
            var firstToken = HashFunctions.SHA512(JsonConvert.SerializeObject(new
            {
                request.Answers,
                request.Password
            }) + request.Password);
            var secondToken = HashFunctions.SHA512(firstToken + request.Identificator);

            var voterQuiz = await _context.VoterQuizzes.Where(vq => vq.Token == request.Token)
                .FirstOrDefaultAsync(cancellationToken);
            voterQuiz.VoterToken = secondToken;
            voterQuiz.Answers = request.Answers.Select(a => new VoterAnswer()
            {
                QuestionId = a.QuestionId,
                AnswerId = a.AnswerId
            }).ToList();
            await _context.SaveAsync(cancellationToken);
            
            await _emailService.SendEmailAsync(EmailTemplates.QuizResult.Build(firstToken, request.Identificator, "http://localhost:8080/quizzes/results".SetQueryParam("token", request.Token)),
                new EmailContact("Voter", voterQuiz.VoterEmail));
            return OperationResult.Ok();
        }
    }