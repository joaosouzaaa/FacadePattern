using FacadePattern.API.Data.DatabaseContexts;
using FacadePattern.API.Data.Repositories.BaseRepositories;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories;

public sealed class InventoryRepository(FacadePatternDbContext dbContext) : BaseRepository<Inventory>(dbContext), IInventoryRepository
{
    public async Task<bool> UpdateQuantityAsync(Inventory inventory) =>
        await DbContextSet.Where(i => i.Id == inventory.Id)
            .ExecuteUpdateAsync(i => i.SetProperty(i => i.Quantity, inventory.Quantity)) > 0;        
}
