using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.Domain.General;

namespace VoiceSharp.ApplicationServices.Quiz.Queries;

public class GetQuizResultsByTokenQuery : IRequest<OperationResult>
{
    public string Token { get; set; }
}

public sealed class GetQuizResultsByTokenQueryHandler : IRequestHandler<GetQuizResultsByTokenQuery, OperationResult>
{
    private readonly IDatabase _database;
    private readonly IMapper _mapper;
    

    public GetQuizResultsByTokenQueryHandler(IDatabase context, IMapper mapper)
    {
        _database = context;
        _mapper = mapper;
    }

    public async Task<OperationResult> Handle(GetQuizResultsByTokenQuery request, CancellationToken cancellationToken)
    {
        var quizId = await _database.VoterQuizzes.Where(vq => vq.Token == request.Token).Select(vq => vq.QuizId).FirstOrDefaultAsync(cancellationToken);

        var results = await _database.VoterQuizzes.Where(vq => vq.QuizId == quizId).Select(vq => new
        {
            Token = vq.VoterToken,
            Answers = vq.Answers.Select(a => new
            {
                Question = a.Question.Name,
                Answer = a.Answer.Text
            })
        }).ToListAsync(cancellationToken);
        
        return OperationResult.Ok(results);
    }
}