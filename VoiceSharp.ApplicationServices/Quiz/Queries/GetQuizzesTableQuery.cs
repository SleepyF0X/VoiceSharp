using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Queries;

public class GetQuizzesTableQuery : IRequest<OperationResult<List<QuizListItem>>>
{
}

public class GetQuizzesTableQueryHandler : IRequestHandler<GetQuizzesTableQuery, OperationResult<List<QuizListItem>>>
{
    private readonly IDatabase _database;
    private readonly IMapper _mapper;

    public GetQuizzesTableQueryHandler(IDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public Task<OperationResult<List<QuizListItem>>> Handle(GetQuizzesTableQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResult.Ok(_database.Quizzes.ProjectTo<QuizListItem>(_mapper.ConfigurationProvider).ToList()));
    }
}