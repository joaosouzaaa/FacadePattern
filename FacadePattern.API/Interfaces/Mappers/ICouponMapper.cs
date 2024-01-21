using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;

namespace FacadePattern.API.Interfaces.Mappers;

public interface ICouponMapper
{
    Coupon SaveToDomain(CouponSave couponSave);
    void UpdateToDomain(CouponUpdate couponUpdate, Coupon coupon);
    List<CouponResponse> DomainListToResponseList(List<Coupon> couponList);
}
