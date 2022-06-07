using FluentValidation;
using MediatR;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Commands;

public class DeleteQuizCommand : IRequest<OperationResult>
{
    public int QuizId { get; set; }
}

public class DeleteQuizCommandValidator : AbstractValidator<DeleteQuizCommand>
{
    public DeleteQuizCommandValidator()
    {
        RuleFor(_ => _.QuizId)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(DeleteQuizCommand.QuizId)));
    }
}

public sealed class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, OperationResult>
{
    private readonly IDatabase _database;
    

    public DeleteQuizCommandHandler(IDatabase context)
    {
        _database = context;
    }

    public async Task<OperationResult> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _database.Quizzes.FindAsync(request.QuizId);
        _database.Quizzes.Remove(quiz);
        await _database.SaveAsync(cancellationToken);
        return OperationResult.Ok();
    }
}