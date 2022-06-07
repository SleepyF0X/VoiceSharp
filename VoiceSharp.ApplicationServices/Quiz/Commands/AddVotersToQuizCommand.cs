using FluentValidation;
using Flurl;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.Models;
using VoiceSharp.Domain.ValidationRules.Auth;
using VoiceSharp.Framework.EmailService;
using VoiceSharp.Framework.EmailService.Constants;
using VoiceSharp.Framework.EmailService.Models;

namespace VoiceSharp.ApplicationServices.Quiz.Commands;

public class AddVotersToQuizCommand : IRequest<OperationResult>
{
    public int QuizId { get; set; }

    public List<string> Emails { get; set; }
}

public class AddVotersToQuizCommandValidator : AbstractValidator<AddVotersToQuizCommand>
{
    public AddVotersToQuizCommandValidator(IRegisterRules rules)
    {
        RuleFor(_ => _.QuizId)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(AddVotersToQuizCommand.QuizId)));
    }
}

public sealed class AddVotersToQuizCommandHandler : IRequestHandler<AddVotersToQuizCommand, OperationResult>
{
    private readonly IDatabase _context;
    private readonly IEmailService _emailService;
    

    public AddVotersToQuizCommandHandler(IDatabase context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<OperationResult> Handle(AddVotersToQuizCommand request, CancellationToken cancellationToken)
    {
        var quizName = await _context.Quizzes.Where(q => q.Id == request.QuizId).Select(q => q.Name)
            .FirstOrDefaultAsync(cancellationToken);
        var existedEmails = await _context.VoterQuizzes
            .Where(vq => vq.QuizId == request.QuizId)
            .Select(vq => vq.VoterEmail)
            .ToListAsync(cancellationToken);

        var emailsToDelete = existedEmails.Except(request.Emails).ToList();
        var emailsToAdd = request.Emails.Except(existedEmails).ToList();
        _context.VoterQuizzes.RemoveRange(_context.VoterQuizzes.Where(vq =>
            vq.QuizId == request.QuizId && emailsToDelete.Contains(vq.VoterEmail)));

        var entities = emailsToAdd.Select(e => new VoterQuiz()
        {
            VoterEmail = e.Trim(),
            QuizId = request.QuizId,
            Token = Guid.NewGuid().ToString(),
        }).ToList();
        await _context.VoterQuizzes.AddRangeAsync(entities, cancellationToken);
        await _context.SaveAsync(cancellationToken);

        foreach (var entity in entities)
        {
            await _emailService.SendEmailAsync(EmailTemplates.QuizLink.Build("http://localhost:8080/quizzes/passing".SetQueryParam("token", entity.Token), quizName),
                new EmailContact("Voter", entity.VoterEmail));
        }
        
        return OperationResult.Ok();
    }
}