using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoiceSharp.API.Controllers.Quizzes.Requests;
using VoiceSharp.ApplicationServices.Quiz.Commands;
using VoiceSharp.ApplicationServices.Quiz.Dtos;
using VoiceSharp.ApplicationServices.Quiz.Queries;
using VoiceSharp.Domain.General;

namespace VoiceSharp.API.Controllers.Quizzes;

[Authorize]
[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuizController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<OperationResult> Create([FromBody] QuizRequestModel quizRequest)
    {
        var registerNewUserCommand = _mapper.Map<CreateQuizCommand>(quizRequest);

        return await _mediator.Send(registerNewUserCommand);
    }
    
    [HttpPut("{quizId:int}/edit")]
    public async Task<OperationResult> Edit([FromBody] QuizRequestModel quizRequest, int quizId)
    {
        var registerNewUserCommand = new EditQuizCommand()
        {
            Quiz = _mapper.Map<QuizDto>(quizRequest),
            QuizId = quizId
        };

        return await _mediator.Send(registerNewUserCommand);
    }
    
    [HttpDelete("{quizId:int}/delete")]
    public async Task<OperationResult> Create(int quizId)
    {
        return await _mediator.Send(new DeleteQuizCommand(){ QuizId = quizId });
    }
    
    [HttpGet]
    public async Task<OperationResult> GetAll()
    {
        var query = new GetQuizzesTableQuery();

        return await _mediator.Send(query);
    }
    
    [HttpGet("{quizId:int}")]
    public async Task<OperationResult> Get(int quizId)
    {
        return await _mediator.Send(new GetQuizByIdQuery(){ QuizId = quizId });
    }
    
    [AllowAnonymous]
    [HttpGet("passing")]
    public async Task<OperationResult> GetByToken(string token)
    {
        return await _mediator.Send(new GetQuizByTokenQuery(){ Token = token });
    }
    
    [HttpPost("{quizId:int}/add-voters")]
    public async Task<OperationResult> AddVoters([FromBody] List<string> emails, int quizId)
    {
        return await _mediator.Send(new AddVotersToQuizCommand(){ QuizId = quizId, Emails = emails});
    }
    
    [HttpGet("{quizId:int}/voters")]
    public async Task<OperationResult> GetVoters(int quizId)
    {
        return await _mediator.Send(new GetQuizVotersByIdQuery(){ QuizId = quizId });
    }
    
    [AllowAnonymous]
    [HttpPost("passing")]
    public async Task<OperationResult> SubmitVote(SubmitVoteRequest request)
    {
        var submitVoteCommand = _mapper.Map<SubmitVoteCommand>(request);

        return await _mediator.Send(submitVoteCommand);
    }
    
    [AllowAnonymous]
    [HttpGet("results")]
    public async Task<OperationResult> GetQuizResultsByToken(string token)
    {
        return await _mediator.Send(new GetQuizResultsByTokenQuery(){ Token = token });
    }
}
