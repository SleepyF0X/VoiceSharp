using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.ApplicationServices.Interfaces.Persistence;

public interface IDatabase
{
    DbSet<User> Users { get; }
    DbSet<Vote> Votes { get; }
    DbSet<Domain.Models.Quiz> Quizzes { get; }
    DbSet<VoterQuiz> VoterQuizzes { get; }
    DbSet<VoterAnswer> VoterAnswers { get; }
    DbSet<Question> Questions { get; }
    DbSet<Answer> Answers { get; }
    Task SaveAsync(CancellationToken token = default);
}