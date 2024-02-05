using FacadePattern.API.Data.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories.BaseRepositories;

public abstract class BaseRepository<TEntity> : IDisposable
    where TEntity : class
{
    protected readonly FacadePatternDbContext _dbContext;
    protected DbSet<TEntity> DbContextSet => _dbContext.Set<TEntity>();

    public BaseRepository(FacadePatternDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        _dbContext.Dispose();

        GC.SuppressFinalize(this);
    }

    protected async Task<bool> SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync() > 0;    
}
