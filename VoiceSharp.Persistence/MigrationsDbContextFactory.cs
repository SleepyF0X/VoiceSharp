using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VoiceSharp.Persistence
{
    internal class MigrationsDbContextFactory : IDesignTimeDbContextFactory<VoiceSharpContext>
    {
        public VoiceSharpContext CreateDbContext(string[] args)
        {
            var connectionString = args.Any()
                ? args[0]
                : @"User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Assessment";

            var builder = new DbContextOptionsBuilder<VoiceSharpContext>();
            builder
                .UseNpgsql(connectionString);
            var context = new VoiceSharpContext(builder.Options);
            return context;
        }
    }
}
