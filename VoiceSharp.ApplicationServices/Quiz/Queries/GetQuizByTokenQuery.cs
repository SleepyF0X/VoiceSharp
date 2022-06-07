using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Queries;

public class GetQuizByTokenQuery : IRequest<OperationResult>
{
    public string Token { get; set; }
}

public sealed class GetQuizByTokenQueryHandler : IRequestHandler<GetQuizByTokenQuery, OperationResult>
{
    private readonly IDatabase _database;
    private readonly IMapper _mapper;
    

    public GetQuizByTokenQueryHandler(IDatabase context, IMapper mapper)
    {
        _database = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetQuizByTokenQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _database.VoterQuizzes
            .Include(q => q.Quiz)
            .ThenInclude(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Where(vq => vq.Token == request.Token)
            .Select(vq => vq.Quiz)
            .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return OperationResult.Ok(quiz);
    }
}