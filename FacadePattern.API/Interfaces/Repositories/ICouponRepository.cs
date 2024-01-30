using FacadePattern.API.Entities;
using System.Linq.Expressions;

namespace FacadePattern.API.Interfaces.Repositories;

public interface ICouponRepository
{
    Task<bool> AddAsync(Coupon coupon);
    Task<Coupon?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Coupon coupon);
    Task<bool> ExistsAsync(Expression<Func<Coupon, bool>> predicate);
    Task<bool> DeleteAsync(int id);
    Task<List<Coupon>> GetAllAsync();
    Task<double> GetDiscountPorcentageByNameAsync(string name);
}
