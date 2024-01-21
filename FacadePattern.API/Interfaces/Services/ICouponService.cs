using FacadePattern.API.DataTransferObjects.Coupon;

namespace FacadePattern.API.Interfaces.Services;

public interface ICouponService
{
    Task<bool> AddAsync(CouponSave couponSave);
    Task<bool> UpdateAsync(CouponUpdate couponUpdate);
    Task<bool> DeleteAsync(int id);
    Task<List<CouponResponse>> GetAllAsync();
}
