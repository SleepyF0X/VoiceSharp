using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.ApplicationServices.Interfaces.Persistence;
using VoiceSharp.Domain.Contracts;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.Persistence;

public class VoiceSharpContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IDatabase
{
    public DbSet<Vote> Votes { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<VoterQuiz> VoterQuizzes { get; set; }
    public DbSet<VoterAnswer> VoterAnswers { get; set; }
    public async Task SaveAsync(CancellationToken token = default)
    {
        await SaveChangesAsync(token);
    }

    public VoiceSharpContext(DbContextOptions<VoiceSharpContext> options)
        : base(options)
#pragma warning restore 8618
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(VoiceSharpContext).Assembly);
    }

    public override int SaveChanges()
    {
        UpdateDates();

        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateDates();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateDates();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateDates()
    {
        var now = DateTime.UtcNow;

        var entries = ChangeTracker
            .Entries()
            .Where(x => x.Entity is IWithDateCreated);

        foreach (var entry in entries)
        {
            var entity = (IWithDateCreated)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.SetDateCreated(now);
            }
            else
            {
                entry.Property(nameof(IWithDateCreated.DateCreated)).IsModified = false;
            }
        }
    }
}