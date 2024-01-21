using FacadePattern.API.Data.DatabaseContexts;
using FacadePattern.API.Data.Repositories.BaseRepositories;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories;

public sealed class ProductRepository(FacadePatternDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
{
    public async Task<bool> AddAsync(Product product)
    {
        await DbContextSet.AddAsync(product);

        return await SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(int id) =>
        DbContextSet.AnyAsync(p => p.Id == id);

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await DbContextSet.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        DbContextSet.Remove(product!);

        return await SaveChangesAsync();
    }

    public Task<List<Product>> GetAllAsync() =>
        DbContextSet.AsNoTracking()
                    .Include(p => p.Inventory)
                    .ToListAsync();

}
