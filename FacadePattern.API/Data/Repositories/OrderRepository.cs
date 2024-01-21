using FacadePattern.API.Data.DatabaseContexts;
using FacadePattern.API.Data.Repositories.BaseRepositories;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacadePattern.API.Data.Repositories;

public sealed class OrderRepository(FacadePatternDbContext dbContext) : BaseRepository<Order>(dbContext), IOrderRepository
{
    public async Task<bool> AddAsync(Order order)
    {
        await DbContextSet.AddAsync(order);

        return await SaveChangesAsync();
    }

    public Task<List<Order>> GetAllAsync() =>
        DbContextSet.AsNoTracking()
                    .Include(o => o.ProductsOrder)
                    .ThenInclude(p => p.Product)
                    .ToListAsync();
}
