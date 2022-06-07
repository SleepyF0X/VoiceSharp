using AutoMapper;
using FluentValidation;
using MediatR;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;
using VoiceSharp.Domain.ValidationRules.Auth;

namespace VoiceSharp.ApplicationServices.Quiz.Commands;

public class CreateQuizCommand : IRequest<OperationResult>
{
    public QuizDto Quiz { get; set; }
}

public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    public CreateQuizCommandValidator(IRegisterRules rules)
    {
        RuleFor(_ => _.Quiz.Name)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(CreateQuizCommand.Quiz.Name)));
    }
}

public sealed class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, OperationResult>
{
    private readonly IDatabase _context;
    private readonly IMapper _mapper;
    

    public CreateQuizCommandHandler(IDatabase context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {
        await _context.Quizzes.AddAsync( _mapper.Map<Domain.Models.Quiz>(request.Quiz), cancellationToken);
        await _context.SaveAsync(cancellationToken);
        return OperationResult.Ok();
    }
}