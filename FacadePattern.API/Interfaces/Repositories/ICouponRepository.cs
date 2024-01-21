using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<bool> AddAsync(Coupon coupon);
    Task<bool> UpdateAsync(Coupon coupon);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Coupon>> GetAllAsync();
}
