using FacadePattern.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories.BaseRepositories;

public abstract class BaseRepository<TEntity>(FacadePatternDbContext dbContext) : IDisposable
    where TEntity : class
{
    protected DbSet<TEntity> DbContextSet => dbContext.Set<TEntity>();

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    protected async Task<bool> SaveChangesAsync() =>
        await dbContext.SaveChangesAsync() > 0;    
}
