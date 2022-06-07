using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.ValidationRules.Auth;

namespace VoiceSharp.ApplicationServices.Quiz.Commands;

public class EditQuizCommand : IRequest<OperationResult>
{
    public int QuizId { get; set; }
    public QuizDto Quiz { get; set; }
}

public class EditQuizCommandValidator : AbstractValidator<EditQuizCommand>
{
    public EditQuizCommandValidator(IRegisterRules rules)
    {
        RuleFor(_ => _.Quiz.Name)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(EditQuizCommand.Quiz.Name)));
    }
}

public sealed class EditQuizCommandHandler : IRequestHandler<EditQuizCommand, OperationResult>
{
    private readonly IDatabase _database;
    private readonly IMapper _mapper;
    

    public EditQuizCommandHandler(IDatabase context, IMapper mapper)
    {
        _database = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(EditQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _database.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == request.QuizId, cancellationToken);
        _mapper.Map(request.Quiz, quiz);
        await _database.SaveAsync(cancellationToken);
        return OperationResult.Ok();
    }
}