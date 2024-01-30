using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Services;

public interface ICouponService
{
    Task<bool> AddAsync(CouponSave couponSave);
    Task<bool> UpdateAsync(CouponUpdate couponUpdate);
    Task<bool> DeleteAsync(int id);
    Task<List<CouponResponse>> GetAllAsync();
    Task<bool> ProcessDiscountAsync(Order order, string name);
}
