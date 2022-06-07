using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Queries;

public class GetQuizVotersByIdQuery : IRequest<OperationResult>
{
    public int QuizId { get; set; }
}

public class GetQuizVotersByIdQueryValidator : AbstractValidator<GetQuizVotersByIdQuery>
{
    public GetQuizVotersByIdQueryValidator()
    {
        RuleFor(_ => _.QuizId)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(GetQuizVotersByIdQuery.QuizId)));
    }
}

public sealed class GetQuizVotersByIdQueryHandler : IRequestHandler<GetQuizVotersByIdQuery, OperationResult>
{
    private readonly IDatabase _database;
    

    public GetQuizVotersByIdQueryHandler(IDatabase context)
    {
        _database = context;
    }

    public async Task<OperationResult> Handle(GetQuizVotersByIdQuery request, CancellationToken cancellationToken)
    {
        var voterEmails = await _database.VoterQuizzes
            .Where(vq => vq.QuizId == request.QuizId)
            .Select(vq => vq.VoterEmail)
            .ToListAsync(cancellationToken);
        return OperationResult.Ok(voterEmails);
    }
}