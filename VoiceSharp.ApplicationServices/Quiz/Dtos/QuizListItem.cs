using VoiceSharp.Domain.Contracts.AutoMapper;

namespace VoiceSharp.ApplicationServices.Quiz.Dtos;

public class QuizListItem : IMapFrom<Domain.Models.Quiz>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}