using FacadePattern.API.DataTransferObjects.Coupon;
using FacadePattern.API.Entities;
using FacadePattern.API.Interfaces.Mappers;

namespace FacadePattern.API.Mappers;

public sealed class CouponMapper : ICouponMapper
{
    public Coupon SaveToDomain(CouponSave couponSave) =>
        new()
        {
            DiscountPorcentage = couponSave.DiscountPorcentage,
            Name = couponSave.Name
        };

    public void UpdateToDomain(CouponUpdate couponUpdate, Coupon coupon)
    {
        coupon.Name = couponUpdate.Name;
        coupon.DiscountPorcentage = couponUpdate.DiscountPorcentage;
    }

    public List<CouponResponse> DomainListToResponseList(List<Coupon> couponList) =>
        couponList.Select(DomainToResponse).ToList();

    private CouponResponse DomainToResponse(Coupon coupon) =>
        new()
        {
            DiscountPorcentage = coupon.DiscountPorcentage,
            Id = coupon.Id,
            Name = coupon.Name
        };
}
