using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.Constants;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Queries;

public class GetQuizByIdQuery : IRequest<OperationResult>
{
    public int QuizId { get; set; }
}

public class GetQuizByIdQueryValidator : AbstractValidator<GetQuizByIdQuery>
{
    public GetQuizByIdQueryValidator()
    {
        RuleFor(_ => _.QuizId)
            .NotEmpty()
            .WithMessage(string.Format(ErrorConstants.FieldIsRequired, nameof(GetQuizByIdQuery.QuizId)));
    }
}

public sealed class GetQuizByIdQueryHandler : IRequestHandler<GetQuizByIdQuery, OperationResult>
{
    private readonly IDatabase _database;
    private readonly IMapper _mapper;
    

    public GetQuizByIdQueryHandler(IDatabase context, IMapper mapper)
    {
        _database = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _database.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == request.QuizId, cancellationToken);
        return OperationResult.Ok(quiz);
    }
}