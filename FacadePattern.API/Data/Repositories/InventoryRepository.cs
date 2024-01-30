using FacadePattern.API.Data.DatabaseContexts;
using FacadePattern.API.Data.Repositories.BaseRepositories;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories;

public sealed class InventoryRepository(FacadePatternDbContext dbContext) : BaseRepository<Inventory>(dbContext), IInventoryRepository
{
    public Task<int> GetQuantityByProductIdAsync(int productId) =>
        DbContextSet.AsNoTracking()
                    .Where(i => i.ProductId == productId)
                    .Select(i => i.Quantity)
                    .FirstOrDefaultAsync();

    public Task UpdateQuantityByProductIdAsync(int productId, int quantity) =>
        DbContextSet.Where(i => i.ProductId == productId)
                    .ExecuteUpdateAsync(i => i.SetProperty(i => i.Quantity, quantity));        
}
