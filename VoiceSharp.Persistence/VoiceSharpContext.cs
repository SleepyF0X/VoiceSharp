using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VoiceSharp.Domain.Contracts;
using VoiceSharp.Domain.Models;

namespace VoiceSharp.Persistence;

public class VoiceSharpContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DbSet<Vote> Votes { get; set; }
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