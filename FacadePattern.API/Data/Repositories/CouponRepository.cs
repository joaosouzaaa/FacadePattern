using FacadePattern.API.Data.DatabaseContexts;
using FacadePattern.API.Data.Repositories.BaseRepositories;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FacadePattern.API.Data.Repositories;

public sealed class CouponRepository(FacadePatternDbContext dbContext) : BaseRepository<Coupon>(dbContext), ICouponRepository
{
    public async Task<bool> AddAsync(Coupon coupon)
    {
        await DbContextSet.AddAsync(coupon);

        return await SaveChangesAsync();
    }

    public Task<Coupon?> GetByIdAsync(int id) =>
        DbContextSet.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);
    
    public Task<bool> UpdateAsync(Coupon coupon)
    {
        _dbContext.Entry(coupon).State = EntityState.Modified;

        return SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Expression<Func<Coupon, bool>> predicate) =>
        DbContextSet.AnyAsync(predicate);

    public async Task<bool> DeleteAsync(int id)
    {
        var coupon = await DbContextSet.FirstOrDefaultAsync(c => c.Id == id);

        DbContextSet.Remove(coupon!);

        return await SaveChangesAsync();
    }

    public Task<List<Coupon>> GetAllAsync() =>
        DbContextSet.AsNoTracking().ToListAsync();

    public Task<double> GetDiscountPorcentageByNameAsync(string name) =>
        DbContextSet.AsNoTracking()
                    .Where(c => c.Name == name)
                    .Select(c => c.DiscountPorcentage)
                    .FirstOrDefaultAsync();
}
