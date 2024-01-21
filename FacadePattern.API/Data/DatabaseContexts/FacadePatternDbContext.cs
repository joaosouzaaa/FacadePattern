using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.DatabaseContexts;

public sealed class FacadePatternDbContext(DbContextOptions<FacadePatternDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FacadePatternDbContext).Assembly);
    }
}
